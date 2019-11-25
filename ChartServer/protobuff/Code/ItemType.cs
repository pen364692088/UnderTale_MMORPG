using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ItemType:byte
{
    Gold=1,
    HealingPotion,
    Food,
    STRPotion,
    DexPotion,
    Chest,
    Knight_woman,
    Fire_woman,
}

public static class ItemMsg
{
    public  static string getItemDescription(ItemType type)
    {
        string str = "";
        switch (type)
        {
            case ItemType.Gold:
                {
                    str = "镀金硬币,广泛流通的货币";
                    break;
                }
            case ItemType.HealingPotion:
                {
                    str = "描述暂无";
                    break;
                }
            case ItemType.Food:
                {
                    str = "描述暂无";
                    break;
                }
            case ItemType.STRPotion:
                {
                    str = "描述暂无";
                    break;
                }
            case ItemType.DexPotion:
                {
                    str = "描述暂无";
                    break;
                }
            default:
                {
                    str = "描述暂无";
                    break;
                }
        }
        return str;
    }

    public static string getURL(ItemType type)
    {
        string str = "";
        switch (type)
        {
            case ItemType.Gold:
                {
                    str = "ui://9m2h6eras6p22n";
                    break;
                }
            case ItemType.HealingPotion:
                {
                    str = "ui://9m2h6eras6p22n";
                    break;
                }
            case ItemType.Food:
                {
                    str = "ui://9m2h6eras6p22n";
                    break;
                }
            case ItemType.STRPotion:
                {
                    str = "ui://9m2h6eras6p22n";
                    break;
                }
            case ItemType.DexPotion:
                {
                    str = "ui://9m2h6eras6p22n";
                    break;
                }
            default:
                {
                    str = "ui://9m2h6eras6p22n";
                    break;
                }
        }
        return str;
    }
}