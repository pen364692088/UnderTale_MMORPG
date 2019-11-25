using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Util.Timera {
    public delegate void TimeDelegate();
   public class TimerModel {
       public int Id;
       public long Time;
       public TimeDelegate timeDelegate;
       public TimerModel(int id,long time,TimeDelegate td) {
           this.Id = id;
           this.Time = time;
           this.timeDelegate = td;
       }
       public void Run(){
           this.timeDelegate();
       }
    }
}
