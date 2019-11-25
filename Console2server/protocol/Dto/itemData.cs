using System;

public class itemData {
    public int type;
    public int num;
    public string name;
    public string text;
    public itemData() {
        type = ItemCode.Money;
        num = 1;
        name = "金币";
        text = "流通的最便宜的货币";
    }
    public itemData(int ty,int nu,string nam,string tx) {
        type =ty;
        num = nu;
        name = nam;
        text = tx;
    }
}
