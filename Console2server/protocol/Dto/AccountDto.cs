﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


      [Serializable]
    public class AccountDto {
        public string Account;
        public string Password;

        public AccountDto() {

        } 

        public AccountDto(string acc, string pwd) {
            this.Account = acc;
            this.Password = pwd;
        }
    }

