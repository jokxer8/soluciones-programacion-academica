format ELF64 executable
entry inicio

segment readable executable

inicio:
    mov rsi, pedir_n
    mov rdx, pedir_n_1
    call imprimir
    call leer_int
    mov r12, rax
    mov al, 10
    call imprimir_caracter

    cmp r12, 1
    jl fuera_rango
    cmp r12, 20
    jg fuera_rango

    mov r13, 1

filas:
    cmp r13, r12
    jg fin

    mov r14, 1

estrellas:
    cmp r14, r13
    jg nueva_linea

    mov al, '*'
    call imprimir_caracter

    inc r14
    jmp estrellas

nueva_linea:
    mov al, 10
    call imprimir_caracter

    inc r13
    jmp filas

fuera_rango:
    mov rsi, error
    mov rdx, error_1
    call imprimir

fin:
    call salir

imprimir:
    mov rax, 1
    mov rdi, 1
    syscall
    ret

imprimir_caracter:
    mov [caracter], al
    mov rsi, caracter
    mov rdx, 1
    call imprimir
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

salir:
    mov rax, 60
    xor rdi, rdi
    syscall

segment readable writeable

memoria rb 64
caracter db 0

pedir_n db 'Digite un # entre 1 - 20: '
pedir_n_1 = $ - pedir_n

error db 'Numero fuera de rango.', 10
error_1 = $ - error