
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

   public class FightRoom {
       public int Id { get; private set; }

       public List<PlayerDto> PlayerList { get; set; }

       public List<int> LeaveList { get; set; }

       public Library cardLibrary;

       public List<CardDto> tableCardList { get; set; } //底牌

       private int Multiple { get; set; } //倍数

       //回合管理类
       private Round round{get;set;}
    
}
