using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    [Serializable]
    public class CardDto {

        public int Id;
        public string Name;
        public int Color;
        public int Weight;

        public  CardDto() {

        }

        public CardDto(int Id,int Color, int Weight) {
            this.Id = Id;
            this.Name = "";
            this.Color = Color;
            this.Weight = Weight;
        }
    
}
