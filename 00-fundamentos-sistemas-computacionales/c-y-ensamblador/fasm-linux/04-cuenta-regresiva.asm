format ELF64 executable
entry inicio

segment readable executable

inicio:
    mov rsi, pedir_m
    mov rdx, pedir_m_1
    call imprimir
    call leer_int
    mov r12, rax
    call salto_linea

    mov rsi, pedir_n
    mov rdx, pedir_n_1
    call imprimir
    call leer_int
    mov r13, rax
    call salto_linea

    cmp r12, r13
    jge continuar

    mov r14, r12
    mov r12, r13
    mov r13, r14

continuar:
    mov rsi, titulo
    mov rdx, titulo_1
    call imprimir

    mov r14, r12

ciclo:
    cmp r14, r13
    jl fin

    mov rax, r14
    call imprimir_int

    cmp r14, r13
    je sin_coma

    mov rsi, coma
    mov rdx, coma_1
    call imprimir

sin_coma:
    dec r14
    jmp ciclo

fin:
    call salto_linea
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

pedir_m db 'Digite un numero entero positivo: '
pedir_m_1 = $ - pedir_m

pedir_n db 'Digite otro numero entero positivo: '
pedir_n_1 = $ - pedir_n

titulo db 'Los numeros entre en ese rango son:', 10
titulo_1 = $ - titulo

coma db ', '
coma_1 = $ - coma