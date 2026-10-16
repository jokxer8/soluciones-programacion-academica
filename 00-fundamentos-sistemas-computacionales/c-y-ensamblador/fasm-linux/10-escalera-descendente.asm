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

    mov r13, r12

filas:
    cmp r13, 1
    jl fin

    mov r14, 0
    mov r15, r12
    sub r15, r13

espacios:
    cmp r14, r15
    jge imprimir_estrellas

    mov al, ' '
    call imprimir_caracter

    inc r14
    jmp espacios

imprimir_estrellas:
    mov r14, 1

estrellas:
    cmp r14, r13
    jg fin_fila

    mov al, '*'
    call imprimir_caracter

    inc r14
    jmp estrellas

fin_fila:
    call salto_linea

    dec r13
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