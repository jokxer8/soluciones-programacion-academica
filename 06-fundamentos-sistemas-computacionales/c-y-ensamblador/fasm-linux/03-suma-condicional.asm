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

    mov r14, r12
    add r14, r13

    cmp r12, r13
    jne imprimir_resultado

    add r14, r14

imprimir_resultado:
    mov rsi, texto_resultado
    mov rdx, texto_resultado_1
    call imprimir

    mov rax, r14
    call imprimir_int
    call salto_linea

    call exit

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

leer_digitos:
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
    jmp leer_digitos

fin_leer:
    ret

imprimir_int:
    mov rsi, numero_memoria + 31
    mov rbx, 10

    cmp rax, 0
    jne convertir
    dec rsi
    mov byte [rsi], '0'
    mov rdx, 1
    jmp imprimirlo

convertir:
    xor rdx, rdx
    div rbx
    add dl, '0'
    dec rsi
    mov [rsi], dl
    test rax, rax
    jne convertir

    mov rdx, numero_memoria + 31
    sub rdx, rsi

imprimirlo:
    call imprimir
    ret

salto_linea:
    mov rsi, nueva_linea
    mov rdx, 1
    call imprimir
    ret

exit:
    mov rax, 60
    xor rdi, rdi
    syscall

segment readable writeable

memoria rb 64
numero_memoria rb 32
nueva_linea db 10

pedir_a db 'Digite el primer valor: '
pedir_a_1 = $ - pedir_a

pedir_b db 'Digite el segundo valor: '
pedir_b_1 = $ - pedir_b

texto_resultado db 'Resultado: '
texto_resultado_1 = $ - texto_resultado