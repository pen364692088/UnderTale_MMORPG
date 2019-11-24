using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    [Serializable]
    public class PlayerDto {
        public int Id;
        public int identity;
        public List<CardDto> cardList;
        public PlayerDto(int uid) {
            this.Id = uid;
            this.identity = -1;
            this.cardList = new List<CardDto>();
        }

        public bool isNoCard {
            get {
                return cardList.Count == 0;
            }
        }
        public int cardCount {
            get {
                return cardList.Count;
            }
        }
        public void AddCard(CardDto card) {
            cardList.Add(card);
        }
        public void RemoveCard(CardDto card) {
            cardList.Remove(card);
        }

    
}
