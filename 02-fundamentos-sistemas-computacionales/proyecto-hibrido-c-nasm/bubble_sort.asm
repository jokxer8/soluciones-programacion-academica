section .text

global bubble_sort

bubble_sort:
    cmp edx, 1
    jle .done

    xor r8d, r8d

.outer_loop:
    xor r9d, r9d

    mov r10d, edx
    sub r10d, r8d
    dec r10d 

.inner_loop:
    cmp r9d, r10d
    jge .next_outer

    mov eax, [rcx + r9 * 4] 
    mov r11d, [rcx + r9 * 4 + 4]

    cmp eax, r11d
    jle .no_swap     

    mov [rcx + r9 * 4], r11d
    mov [rcx + r9 * 4 + 4], eax  

.no_swap:
    inc r9d
    jmp .inner_loop

.next_outer:
    inc r8d

    mov r10d, edx
    dec r10d

    cmp r8d, r10d
    jl .outer_loop

.done:
    ret