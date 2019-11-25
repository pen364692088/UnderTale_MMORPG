using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    //角色数据模型
    public class UserModel {

        public int Id;
        public string Name;
      
        public int Gold;
        public int Level;
        public int Exp;

       

        public int AccountId; //外键

        public UserModel(int Id,string Name,int AccountId) {
            this.Id = Id;
            this.Name = Name;
            this.AccountId = AccountId;

            this.Gold = 1500;
            this.Level = 1;
            this.Exp = 0;
        
        }

    
}
