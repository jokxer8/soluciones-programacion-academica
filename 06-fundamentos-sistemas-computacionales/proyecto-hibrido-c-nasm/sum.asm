section .text

global sum

sum:
    xor eax, eax      

    cmp edx, 0
    jle .done       

    xor r8d, r8d   

.loop:
    add eax, [rcx + r8 * 4]

    inc r8d                

    cmp r8d, edx
    jl .loop               

.done:
    ret