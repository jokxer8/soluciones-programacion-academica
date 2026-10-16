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
    mov rax, 0
    mov rdi, 0
    mov rsi, memoria
    mov rdx, 64
    syscall

    mov rcx, rax
    mov rsi, memoria
    xor rax, rax
    xor r8, r8

buscar:
    cmp rcx, 0
    je fin_leer

    mov bl, [rsi]

    cmp bl, '-'
    je negativo

    cmp bl, '0'
    jb avanzar
    cmp bl, '9'
    jbe leer_digitos

avanzar:
    inc rsi
    dec rcx
    jmp buscar

negativo:
    mov r8, 1
    inc rsi
    dec rcx

leer_digitos:
    cmp rcx, 0
    je aplicar_signo

    mov bl, [rsi]
    cmp bl, '0'
    jb aplicar_signo
    cmp bl, '9'
    ja aplicar_signo

    imul rax, rax, 10
    movzx rbx, bl
    sub rbx, '0'
    add rax, rbx

    inc rsi
    dec rcx
    jmp leer_digitos

aplicar_signo:
    cmp r8, 1
    jne fin_leer
    neg rax

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