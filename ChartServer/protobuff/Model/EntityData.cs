using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class EntityData
{
    public int id;
    public Object type;
    public int health;
    public int nowHealth;

    public int STR=1;
    public int DEX=1;
    public int LUK=1;

    public float x;
    public float y;
    public float z;

    public ItemPackage failItem = new ItemPackage();

    public virtual void AddItem(Queue<ItemData> list)
    {
        while (list.Count > 0)
        {
            failItem.itemList.Enqueue(list.Dequeue());
        }
        
    }

    public virtual void AddItem(ItemData item)
    {
        failItem.itemList.Enqueue(item);
    }

    public virtual void setHpNormal()
    {
        health = 200;
        nowHealth = 200;
    }
    public virtual void setAttrNorml()
    {
        STR = 8;
        DEX = 8;
        LUK = 8;
    }
    public virtual void setPosNorml()
    {
        x = 0;
        y = 0;
        z = 0;
    }
    public virtual void setPos(float _x,float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
    public void beAttacked(int damage)
    {
        nowHealth -= damage;
    }
   
}
