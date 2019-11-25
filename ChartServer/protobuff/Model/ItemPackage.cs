using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class ItemPackage
{
    public Queue<ItemData> itemList = new Queue<ItemData>();
    public ItemPackage()
    {
        itemList = new Queue<ItemData>();
    }
    public ItemPackage(Queue<ItemData> list)
    {
        itemList =list;
    }
}
