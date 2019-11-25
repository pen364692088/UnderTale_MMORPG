using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class MonsterData
 {
    public int type;
    public int health;
    public TransData transdata;
    public MonsterData()
    {

    }
    public MonsterData(int t,int h,TransData trans)
    {
        type = t;
        health = h;
        transdata = trans;
    }
}
