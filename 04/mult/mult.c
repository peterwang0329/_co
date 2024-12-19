#include<stdio.h>



int main(int R0 ,int R1){
    R0 = 3;
    R1 = 4;
    int R2 = 0; //@R2,M=0
    LOOP:
        if(R0 >= 0) goto LOOP; //@R0,D=M, @LOOP,0;JLE
        R2+=R1; //@R1,D=M , @R2,M=M+D
        R0--;    //@R0,M=M-1
        goto LOOP; //@0 D=M, @LOOP, 0;JGT
    END:
        printf("%d",R2); //@END,0;JMP
}