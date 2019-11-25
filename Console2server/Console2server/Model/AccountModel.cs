using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public    class AccountModel {

        public int Id;
        public string Account;
        public string Password;
        public AccountModel(int id, string acc, string psw) {
            this.Id = id;
            this.Account = acc;
            this.Password = psw;
        }
    
}
