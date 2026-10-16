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

    cmp r12, 2
    jl no_primo

    mov r13, 2

ciclo_primo:
    mov rax, r13
    imul rax, r13
    cmp rax, r12
    jg si_primo

    mov rax, r12
    cqo
    idiv r13

    cmp rdx, 0
    je no_primo

    inc r13
    jmp ciclo_primo

si_primo:
    mov rsi, mensaje_primo
    mov rdx, mensaje_primo_1
    call imprimir
    call salir

no_primo:
    mov rsi, mensaje_no_primo
    mov rdx, mensaje_no_primo_1
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

salir:
    mov rax, 60
    xor rdi, rdi
    syscall

segment readable writeable

memoria rb 64
nueva_linea db 10

pedir_n db 'Digite un numero entero positivo: '
pedir_n_1 = $ - pedir_n

mensaje_primo db 'El numero es primo.', 10
mensaje_primo_1 = $ - mensaje_primo

mensaje_no_primo db 'El numero no es primo.', 10
mensaje_no_primo_1 = $ - mensaje_no_primo