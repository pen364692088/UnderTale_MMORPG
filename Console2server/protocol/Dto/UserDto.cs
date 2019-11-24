using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


     [Serializable]
    public class UserDto {

        public int Id;
        public string Name;

         public int Gold;
        public int Level;
        public int Exp;
    
        public UserDto() {
          
        }
        public UserDto(string Name) {
           
            this.Name = Name;
        }
        public UserDto(int id, string Name, int Gold, int Level, int Exp) {
            this.Id = id;
            this.Name = Name;
            this.Gold = Gold;
            this.Level = Level;
            this.Exp = Exp;
        }

    
}
