using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class RolerData:EntityData
{
 
    public  int rid;
    public new RoleType type;
    public string name;
    public int level;
    public int exp;


    public Dictionary<int, ItemData> itemBag = new Dictionary<int, ItemData>();

    public Dictionary<int, ItemData> quickItem = new Dictionary<int, ItemData>();

    public Dictionary<SkillCode, int> SkillList = new Dictionary<SkillCode, int>();

    public RolerData()
    {
       
        id = -1;
        rid = -1;

        type =RoleType.KnightMale;
        name = "无名氏";
        level = 1;
        exp = 0;
       

        itemBag = new Dictionary<int, ItemData>();
        quickItem = new Dictionary<int, ItemData>();
        SkillList = new Dictionary<SkillCode, int>();

        setAttrNorml();
        setHpNormal();
        setPosNorml();

        AddItem(new ItemData());
        AddItem(new ItemData());
    }

    public RolerData(int uid,int _rid, Dictionary<string, object> dic)
    {
        id = uid;
        rid = _rid;

        type =(RoleType) dic["Type"] ;
        name = dic["Name"].ToString();
        level = (int)dic["Level"];
        exp = (int)dic["Exp"];
        health = nowHealth = (int)dic["Health"];
        STR = (int)dic["STR"];
        DEX = (int)dic["DEX"];
        LUK = (int)dic["LUK"];
        itemBag = new Dictionary<int, ItemData>();
        quickItem = new Dictionary<int, ItemData>();
        SkillList = new Dictionary<SkillCode, int>();

        x = (float)dic["PosX"];
        y = (float)dic["PosY"];
        z = (float)dic["PosZ"];

        AddItem(new ItemData());
        AddItem(new ItemData());
    }

    public RolerData(RolerData data)
    {
        id = data.id;
        rid = data.rid;

        type = data.type;
        name = data.name;
        level = data.level;
        exp = data.exp;
        health = data.health;
        nowHealth = data.nowHealth;
        
        STR = data.STR;
        DEX = data.DEX;
        LUK = data.LUK;

        //浅克隆 TODO
        itemBag =  data.itemBag;
        quickItem = data.quickItem;
        SkillList = data.SkillList;
        failItem = data.failItem;

        x = data.x;
        y = data.y;
        z = data.z;

        AddItem(new ItemData());
        AddItem(new ItemData());
    }
    public void AddBagItem(ItemData item)
    {
        if (itemBag.ContainsKey(item.id))
        {
            itemBag.Add(item.id, item);
        }
      
    }
}
