using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class socketMsg {
    public int OpCode { get; set; }

    public int SubCode { set; get; }

    public object value { set; get; }

    public socketMsg() {

    }
    public socketMsg(int O, int S, object V) {
        this.OpCode = O;
        this.SubCode = S;
        this.value = V;
    }
    public void change(int O, int S, object V) {
        this.OpCode = O;
        this.SubCode = S;
        this.value = V;
    }
}