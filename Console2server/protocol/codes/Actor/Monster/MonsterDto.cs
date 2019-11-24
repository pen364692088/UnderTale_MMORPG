using System;
using System.Collections.Generic;

public class MonsterDto {
    public int health;
    public int nowHealth;

    public int monsterType;

    public Dictionary<int, int> itemList = new Dictionary<int, int>();

    public MonsterDto() {
        health = nowHealth = 50;
        monsterType = MonsterType.Human;
        itemList.Add(ItemCode.Money,100);
    }
}
