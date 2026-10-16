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

    mov rsi, titulo
    mov rdx, titulo_1
    call imprimir
    
    mov r13, 2

    mov r15, 1

ciclo_numeros:
    cmp r13, r12
    jg fin

    mov rax, r13
    call es_primo

    cmp rax, 1
    jne siguiente_numero

    cmp r15, 1
    je imprimir_primo

    mov rsi, coma
    mov rdx, coma_1
    call imprimir

imprimir_primo:
    mov rax, r13
    call imprimir_int
    mov r15, 0

siguiente_numero:
    inc r13
    jmp ciclo_numeros

fin:
    call salto_linea
    call salir

es_primo:
    cmp rax, 2
    jl no_es_primo

    mov r10, rax
    mov r11, 2

ciclo_divisores:
    mov rax, r11
    imul rax, r11
    cmp rax, r10
    jg es_primo_si

    mov rax, r10
    cqo
    idiv r11

    cmp rdx, 0
    je no_es_primo

    inc r11
    jmp ciclo_divisores

es_primo_si:
    mov rax, 1
    ret

no_es_primo:
    xor rax, rax
    ret

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

fin_leer:
    ret

imprimir_int:
    mov rsi, numero_memoria + 32
    mov rbx, 10
    xor r9, r9

    cmp rax, 0
    jne verificar_signo

    dec rsi
    mov byte [rsi], '0'
    mov rdx, 1
    jmp imprimir_numero

verificar_signo:
    cmp rax, 0
    jge convertir

    neg rax
    mov r9, 1

convertir:
    xor rdx, rdx
    div rbx

    add dl, '0'
    dec rsi
    mov [rsi], dl

    test rax, rax
    jne convertir

    cmp r9, 1
    jne calcular_longitud

    dec rsi
    mov byte [rsi], '-'

calcular_longitud:
    mov rdx, numero_memoria + 32
    sub rdx, rsi

imprimir_numero:
    call imprimir
    ret

salto_linea:
    mov rsi, nueva_linea
    mov rdx, 1
    call imprimir
    ret

salir:
    mov rax, 60
    xor rdi, rdi
    syscall

segment readable writeable

memoria rb 64
numero_memoria rb 32
nueva_linea db 10

pedir_n db 'Digite un numero entero positivo: '
pedir_n_1 = $ - pedir_n

titulo db 'Los numeros primos entre 1 y N son:', 10
titulo_1 = $ - titulo

coma db ', '
coma_1 = $ - coma