using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ItemData:EntityData
{

    public new ItemType type;
    public int num;
    public string description;

    public ItemData()
    {
        type = ItemType.Gold;
        num = 1;
        description = ItemMsg.getItemDescription(ItemType.Gold);

        setAttrNorml();
        setHpNormal();
        setPosNorml();
    }
    public ItemData(int _id,ItemType t,int n)
    {
        id = _id;
        type = t;
        num = n;
        description= ItemMsg.getItemDescription(t);

        setAttrNorml();
        setHpNormal();
        setPosNorml();
    }
    public ItemData(int _id, ItemType t, int n, float _x, float _y, float _z)
    {
        id = _id;
        type = t;
        num = n;
        description = ItemMsg.getItemDescription(t);
        setAttrNorml();
        setHpNormal();


        x = _x;
        y = _y;
        z = _z;

        
    }
    public ItemData change(int _id, ItemType t, int n)
    {
        id = _id;
        type = t;
        num = n;
        description = ItemMsg.getItemDescription(t);

        setAttrNorml();
        setHpNormal();
        setPosNorml();

        return this;
    }

    public ItemData change(int _id, ItemType t, int n,float _x,float _y,float _z)
    {
       

        return new ItemData( _id,  t,  n,  _x,  _y,  _z);
    }
    public ItemData setId(int _id)
    {
        id = _id;
        return this;
    }
}
