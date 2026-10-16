format ELF64 executable
entry inicio

segment readable executable

inicio:
    mov rsi, pedir_n
    mov rdx, pedir_n_1
    call imprimir

    call leer_int
    mov r12, rax

    call salto_linea

    cmp r12, 1
    jl fuera_rango
    cmp r12, 20
    jg fuera_rango

    mov r13, 1

creciente:
    cmp r13, r12
    jg preparar_decreciente

    mov r14, 1

estrellas_creciente:
    cmp r14, r13
    jg nueva_linea_creciente

    mov al, '*'
    call imprimir_caracter

    inc r14
    jmp estrellas_creciente

nueva_linea_creciente:
    mov al, 10
    call imprimir_caracter

    inc r13
    jmp creciente

preparar_decreciente:
    mov r13, r12
    dec r13

decreciente:
    cmp r13, 1
    jl fin

    mov r14, 1

estrellas_decreciente:
    cmp r14, r13
    jg nueva_linea_decreciente

    mov al, '*'
    call imprimir_caracter

    inc r14
    jmp estrellas_decreciente

nueva_linea_decreciente:
    mov al, 10
    call imprimir_caracter

    dec r13
    jmp decreciente

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
    mov rax, 0
    mov rdi, 0
    mov rsi, memoria
    mov rdx, 64
    syscall

    mov rcx, rax
    mov rsi, memoria
    xor rax, rax

leer:
    cmp rcx, 0
    je fin_leer

    mov bl, [rsi]
    cmp bl, '0'
    jb avanzar
    cmp bl, '9'
    ja fin_leer

    imul rax, rax, 10
    movzx rbx, bl
    sub rbx, '0'
    add rax, rbx

avanzar:
    inc rsi
    dec rcx
    jmp leer

fin_leer:
    ret

salto_linea:
    mov al, 10
    call imprimir_caracter
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