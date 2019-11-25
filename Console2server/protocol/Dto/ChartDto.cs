using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    [Serializable]
    public class ChartDto {

        public int uid;
        public string text;

        public int voiceId;

        public ChartDto() {
            uid = -1;
            text = "";
            voiceId = -1;
        }

        public ChartDto(int uid,string text,int id) {
            this.uid = uid;
            this.text = text;
            this.voiceId = id;
        }
        public void change(string text, int id) {
            this.text = text;
            this.voiceId = id;
        }
        public void changeUid(int id) {
            
            this.uid = id;
        }

   
}
