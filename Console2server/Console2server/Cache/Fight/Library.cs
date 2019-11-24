using protocol.codes.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class Library {
       //牌库

       public Queue<CardDto> CardLibrary;

       public Library(){
        
           
           init();
       }
       public void init() {
           creat();
           shuffle();
       }
        public void creat(){
            CardLibrary = new Queue<CardDto>();
            int index=0;
            for (int color = CardColor.CLUE; color <= CardColor.SQUERE; color++) {
                for (int weight = CardWeight.THREE; weight <= CardWeight.TWO; weight++) {
                    CardDto card = new CardDto(index, color, weight);
                    CardLibrary.Enqueue(card);
                    index++;
                }
            }
            CardDto sJoker = new CardDto(index++, CardColor.NONE, CardWeight.SJOKER);
            CardDto lJoker = new CardDto(index++, CardColor.NONE, CardWeight.LJOKER);
            CardLibrary.Enqueue(sJoker);
            CardLibrary.Enqueue(lJoker);
        }
        
        public void shuffle() {
            List<CardDto> list = new List<CardDto>();
            Random r = new Random();
            foreach (var card in CardLibrary) {
                int index = r.Next(0, list.Count + 1);
                list.Insert(index, card);
            }
            CardLibrary.Clear();
            foreach (var card in list) {
                CardLibrary.Enqueue(card);
            }
        }

    
}
