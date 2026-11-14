format ELF64 executable
entry inicio

segment readable executable

inicio:
    mov rax, 1
    mov rdi, 1
    mov rsi, mensaje
    mov rdx, mensaje_1
    syscall

    mov rax, 60
    xor rdi, rdi
    syscall

segment readable writeable

mensaje db 'Saludos nuevo mundo!', 10
mensaje_1 = $ - mensaje