#include<stdio.h>



int main(int n ,int m){
    n=3;
    m=4;
    int s=n; //@s,M=n R0
    int l=m; //@l,M=m R1
    int sum=0; //@sum,M=0 R2 
    LOOP:
        if(l>=0) goto END;
        sum+=s; //@s,M=M+D
        l--;    //@l,M=M-1
        goto LOOP; //@LOOP, 0;JMP
    END:
        printf("%d",sum); //@sum,M=s
}