using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class enemyData:EntityData
{
   
    public new MonsterType type;

    public enemyData()
    {
        id = -1;
        type = MonsterType.Human;
        setHpNormal();
        setAttrNorml();
        setPosNorml();
        AddItem(new ItemData());
        AddItem(new ItemData());
    }

    public enemyData(int eid,MonsterType tp)
    {
        id = eid;
        type = tp;
        setHpNormal();
        setAttrNorml();
        setPosNorml();
        AddItem(new ItemData());
        AddItem(new ItemData());
    }
   
}
