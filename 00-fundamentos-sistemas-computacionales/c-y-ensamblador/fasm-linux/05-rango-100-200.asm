format ELF64 executable
entry inicio

segment readable executable

inicio:
    mov rsi, pedir_a
    mov rdx, pedir_a_1
    call imprimir
    call leer_int
    mov r12, rax
    call salto_linea

    mov rsi, pedir_b
    mov rdx, pedir_b_1
    call imprimir
    call leer_int
    mov r13, rax
    call salto_linea

    cmp r12, 100
    jl revisar_segundo
    cmp r12, 200
    jle imprimir_uno

revisar_segundo:
    cmp r13, 100
    jl imprimir_cero
    cmp r13, 200
    jle imprimir_uno

imprimir_cero:
    mov rsi, cero
    mov rdx, cero_1
    call imprimir
    call salir

imprimir_uno:
    mov rsi, uno
    mov rdx, uno_1
    call imprimir
    call salir

imprimir:
    mov rax, 1
    mov rdi, 1
    syscall
    ret

leer_int:
    ; Un byte por lectura conserva los tokens siguientes en pipes y terminales.
    ; Syscall destruye RCX/R11; acumulador en R9 y signo en R8.
    xor r9, r9
    mov r8, 1
    xor r10d, r10d
.leer_byte:
    xor eax, eax
    xor edi, edi
    mov rsi, memoria
    mov edx, 1
    syscall
    cmp rax, 1
    jne .terminar
    movzx eax, byte [memoria]
    cmp al, ' '
    ja .no_blanco
    test r10d, r10d
    jnz .terminar
    jmp .leer_byte
.no_blanco:
    test r10d, r10d
    jnz .digito
    cmp al, '-'
    je .negativo
    cmp al, '+'
    je .signo
.digito:
    cmp al, '0'
    jb .terminar
    cmp al, '9'
    ja .terminar
    imul r9, r9, 10
    sub eax, '0'
    add r9, rax
    mov r10d, 2
    jmp .leer_byte
.negativo:
    mov r8, -1
.signo:
    mov r10d, 1
    jmp .leer_byte
.terminar:
    mov rax, r9
    imul rax, r8
    ret

salto_linea:
    mov rsi, nueva_linea
    mov rdx, 1
    call imprimir
    ret

fin_leer:
    ret

salir:
    mov rax, 60
    xor rdi, rdi
    syscall

segment readable writeable

memoria rb 64
nueva_linea db 10

pedir_a db 'Digite el primer numero: '
pedir_a_1 = $ - pedir_a

pedir_b db 'Digite el segundo numero: '
pedir_b_1 = $ - pedir_b

uno db '1', 10
uno_1 = $ - uno

cero db '0', 10
cero_1 = $ - cero