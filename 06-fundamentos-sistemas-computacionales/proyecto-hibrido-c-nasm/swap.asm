section .text

global swap

swap:
    mov eax, [rcx]  
    mov r8d, [rdx]  

    mov [rcx], r8d 
    mov [rdx], eax 

    ret