using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace protocol.codes.Card {
   public class CardColor {
       public const int NONE = 0;
       /// <summary>
       /// 第1 梅花
       /// </summary>
       public const int CLUE = 1;
       /// <summary>
       /// 第2 红桃
       /// </summary>
       public const int HEART = 2;
       /// <summary>
       /// 第3 黑桃
       /// </summary>
       public const int SPADE = 3; 
       /// <summary>
       /// 第4 方块
       /// </summary>
       public const int SQUERE = 4;

       public static string GetString(int color){
           switch(color){
               case CLUE:{
                   return "CLUE";
                  
                   }
               case HEART: {
                   return "HEART";
                      
                   }
               case SPADE: {
                   return "SPADE";
                  
                   }
               case SQUERE: {
                   return "SQUERE";

                   }
               default: {
                   throw new Exception("不存在"+color+"花色");
                   }
           }
       }
    }
}
