using server.Util.Current;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace server.Util.Timera {
    public class TimerManager {
        private static TimerManager instance = null;
        public static TimerManager Instance {
            get {
                lock (instance) {
                    if (instance ==null) {
                        instance = new TimerManager();
                    }
                    return instance;
                }
            }
        }

        private ConcurrentDictionary<int, TimerModel> dic = new ConcurrentDictionary<int, TimerModel>();
        private List<int> removeList=new List<int>();

        /*
        private void  Timer_Elaspe(object sender,ElapsedEventArgs e){
            throw new NotImplementedException();
        } */
        private CurrentInt id = new CurrentInt(-1);
        //Timer类
        private System.Timers.Timer Time;
        public TimerManager() {
            Time = new System.Timers.Timer(10);
            Time.Elapsed += Time_Elapsed;
        }

        void Time_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
           
            lock (removeList) {
                TimerModel tModel = null;
                foreach (var id in removeList) {
                    dic.TryRemove(id, out tModel);
                }

                removeList.Clear();
                foreach (var id in dic.Keys) {
                    removeList.Add(id);
                }
            }
           
            foreach(var model in dic.Values){
                if (model.Time <= DateTime.Now.Ticks) {
                    model.Run();
                }
              
            }
           
        }
        //日期时间
        public void AddTimerEvent(DateTime endTime, TimeDelegate td) {
            long delayTime = endTime.Ticks - DateTime.Now.Ticks;
            if (delayTime <= 0) {
                return;
            }
            AddTimeEvent(delayTime, td);
        }
        //延迟时间
        //毫秒
        public void AddTimeEvent(long time, TimeDelegate td) {
            TimerModel model = new TimerModel(id.Add_Get(), DateTime.Now.Ticks + time * 1000, td);
            dic.TryAdd(model.Id, model);

        }
    }
}
