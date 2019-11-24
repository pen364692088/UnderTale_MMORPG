using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace protocol.codes.Card {
   
   public  class CardWeight {
       public const int THREE = 3;
       public const int FOUR = 4;
       public const int FIVE = 5;
       public const int SIX = 6;
       public const int SEVEN = 7;
       public const int EIGHT = 8;
       public const int NINE = 9;
       public const int TEN = 10;

       public const int JACK = 11;
       public const int QUEEN = 12;
       public const int KING = 13;

       public const int ONE = 14;
       public const int TWO = 15;

       public const int SJOKER = 16;
       public const int LJOKER = 17;

       public static string GetString(int weight) {
           switch (weight) {
               case THREE: {
                   return "THREE";

                   }
               case FOUR: {
                   return "FOUR";

                   }
               case FIVE: {
                   return "FIVE";

                   }
               case SIX: {
                   return "SIX";

                   }
               case SEVEN: {
                   return "SEVEN";

                   }
               case EIGHT: {
                   return "EIGHT";

                   }
            
               case NINE: {
                   return "NINE";

                   }
               case TEN: {
                       return "TEN";

                   }
               case JACK: {
                   return "JACK";

                   }
               case QUEEN: {
                   return "QUEEN";

                   }
               case KING: {
                   return "KING";

                   }
               case ONE: {
                   return "ONE";

                   }
               case TWO: {
                   return "TWO";

                   }
               case SJOKER: {
                   return "SJOKER";

                   }
               case LJOKER: {
                       return "LJOKER";

                   }
               default: {
                       throw new Exception("不存在" + weight + "花色");
                   }
           }
       }
    }
}
