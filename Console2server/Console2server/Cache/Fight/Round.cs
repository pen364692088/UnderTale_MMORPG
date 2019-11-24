using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Round {
        public int CurrentUid;
        public int BiggestUid;
        public int LastCardWeight;
        public int LastCardLength;
        public int LastCardType;

        public Round() {
            CurrentUid = -1;
            BiggestUid = -1;
            LastCardWeight = -1;
            LastCardLength = -1;
            LastCardType = -1;
        }
        public void Start(int uid) {
            CurrentUid = uid;
            BiggestUid = uid;
        }
        public void Change(int uid,int weight,int length,int type){
            BiggestUid=uid;
            LastCardWeight=weight;
            LastCardLength = length;
            LastCardType = type;
         }
        public void Turn(int uid) {
            CurrentUid = uid;
        }
    
}
