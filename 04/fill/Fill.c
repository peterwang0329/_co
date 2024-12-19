#include <stdio.h>

int main(){
    FOREVER:
    int i = 16384;  //  @16384, D=A, @i, M=D  //初始化 16384~24575為螢幕
    int M[100000];

    WHILE:
        if (i >= 24576)  //   @24576, D=A, @i, D=M-D    註：(D=i-24576)
            goto WEND;     //   @WEND, D;JGE
        int color = 0;   //   @color, M=0   
        if (M[24576]==0) //   @24576, D=M
        goto NEXT;     //   @NEXT, D;JEQ
        color = -1;      //   @color, M=-1  // 黑色
    NEXT:
        M[i] = color;    //   @color, D=M, @i, A=M, M=D
        i++;             //   @i, M=M+1
        goto WHILE;      //   @WHILE, 0;JMP
    WEND:
        goto FOREVER;      //   @FOREVER, 0;JMP
}