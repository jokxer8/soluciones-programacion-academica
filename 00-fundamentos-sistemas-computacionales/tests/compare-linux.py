"""Pruebas posteriores a la entrega: C y FASM reales, stdin por pipe y PTY Linux."""
import json
import os
from pathlib import Path
import platform
import pty
import re
import subprocess
import tempfile
import termios

ROOT = Path(__file__).resolve().parents[1]
RESULTS = []


def run(exe, values, mode):
    data = ("\n".join(map(str, values)) + "\n").encode() if values else b""
    if mode == "pipe":
        proc = subprocess.run([str(exe)], input=data, capture_output=True, timeout=3)
    else:
        master, slave = pty.openpty()
        attrs = termios.tcgetattr(slave)
        attrs[3] &= ~termios.ECHO
        termios.tcsetattr(slave, termios.TCSANOW, attrs)
        child = subprocess.Popen([str(exe)], stdin=slave, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
        os.close(slave)
        try:
            os.write(master, data)
            try:
                out, err = child.communicate(timeout=3)
            except subprocess.TimeoutExpired:
                child.kill()
                child.communicate()
                raise
            proc = subprocess.CompletedProcess(child.args, child.returncode, out, err)
        finally:
            os.close(master)
    if proc.returncode:
        raise AssertionError(f"exit={proc.returncode}: {proc.stderr.decode(errors='replace')}")
    return proc.stdout.decode(errors="strict").replace("\r\n", "\n")


def canonical(number, text):
    # Solo se retiran preguntas y saltos vacios de presentación; los espacios
    # de alineación de los patrones se conservan. Primo tiene distinto texto.
    text = re.sub(r"Digite [^:\n]*: ", "", text).strip("\n")
    if number == 2:
        values = re.findall(r"(?:Suma|Resta|Producto): (-?\d+)", text)
        assert len(values) == 3, text
        return tuple(map(int, values))
    if number == 3:
        return int(re.search(r"Resultado: (-?\d+)", text)[1])
    if number in (4, 7):
        return tuple(map(int, re.findall(r"-?\d+", text.split("son:", 1)[1])))
    if number == 5:
        assert text in ("0", "1"), text
        return int(text)
    if number == 6:
        assert "es primo." in text, text
        return "no es primo." not in text
    return text


def expected(number, values):
    if number == 1:
        return "Saludos nuevo mundo!"
    n = values[0]
    if number == 2:
        a, b = values
        return a + b, a - b, a * b
    if number == 3:
        a, b = values
        return (a + b) * (2 if a == b else 1)
    if number == 4:
        return tuple(range(max(values), min(values) - 1, -1))
    if number == 5:
        return int(any(100 <= value <= 200 for value in values))
    def prime(x):
        return x >= 2 and all(x % i for i in range(2, int(x ** .5) + 1))
    if number == 6:
        return prime(n)
    if number == 7:
        return tuple(x for x in range(2, n + 1) if prime(x))
    if not 1 <= n <= 20:
        return "Numero fuera de rango."
    if number == 8:
        return "\n".join("".join("*" if r in (0, n-1) or c in (0, n-1) else " " for c in range(n)) for r in range(n))
    if number == 9:
        return "\n".join("*" * r for r in range(1, n + 1))
    if number == 10:
        return "\n".join(" " * (n-r) + "*" * r for r in range(n, 0, -1))
    rows = list(range(1, n + 1)) + list(range(n - 1, 0, -1))
    return "\n".join((" " * (n-r) if number == 12 else "") + "*" * r for r in rows)


CASES = {
    1: [()],
    2: [(5, 3), (-5, 3), (0, 0), (40000, 40000), (-40000, 40000)],
    3: [(4, 4), (4, 3), (-4, -4), (0, 0)],
    4: [(15, 6), (6, 15), (5, 5)],
    5: [(99, 100), (200, 201), (99, 201), (100, 200), (-1, 150)],
    6: [(n,) for n in (-1, 0, 1, 2, 13, 25, 97)],
    7: [(n,) for n in (0, 1, 2, 20, 50)],
    **{i: [(n,) for n in (-1, 0, 1, 2, 3, 20, 21)] for i in range(8, 13)},
}

with tempfile.TemporaryDirectory(prefix="fsc-linux-") as tmp:
    for c_file in sorted((ROOT / "c-y-ensamblador/c").glob("*.c")):
        number = int(c_file.name[:2])
        asm_file = ROOT / "c-y-ensamblador/fasm-linux" / (c_file.stem + ".asm")
        c_exe = Path(tmp) / (c_file.stem + "-c")
        asm_exe = Path(tmp) / (c_file.stem + "-asm")
        subprocess.run(["gcc", "-std=c17", "-Wall", "-Wextra", "-Werror", str(c_file), "-o", str(c_exe)], check=True)
        subprocess.run(["fasm", str(asm_file), str(asm_exe)], check=True)
        asm_exe.chmod(0o755)
        for values in CASES[number]:
            for mode in ("pipe", "pty"):
                row = {"program": c_file.stem, "input": values, "mode": mode}
                try:
                    c = canonical(number, run(c_exe, values, mode))
                    asm = canonical(number, run(asm_exe, values, mode))
                    want = expected(number, values)
                    assert c == want, f"C={c!r}; esperado={want!r}"
                    assert asm == want, f"FASM={asm!r}; esperado={want!r}"
                    row["status"] = "PASS"
                except (AssertionError, subprocess.TimeoutExpired, UnicodeError, IndexError, TypeError) as error:
                    row.update(status="FAIL", error=str(error)[:1200])
                    print("FAIL", row, flush=True)
                RESULTS.append(row)
report = {
    "system": platform.platform(),
    "commit": os.environ.get("GITHUB_SHA", "local"),
    "gcc": subprocess.check_output(["gcc", "--version"], text=True).splitlines()[0],
    "cases": RESULTS,
    "passed": sum(r["status"] == "PASS" for r in RESULTS),
    "failed": sum(r["status"] == "FAIL" for r in RESULTS),
}
Path("linux-results.json").write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n")
summary = f"Linux: {report['passed']} comparaciones correctas, {report['failed']} fallidas; 12 pares C/FASM."
print(summary)
if os.environ.get("GITHUB_STEP_SUMMARY"):
    with open(os.environ["GITHUB_STEP_SUMMARY"], "a") as stream:
        stream.write(summary + "\n\nCada caso contrasta ambos programas con un resultado independiente, por pipe y terminal PTY.\n")
raise SystemExit(bool(report["failed"]))
