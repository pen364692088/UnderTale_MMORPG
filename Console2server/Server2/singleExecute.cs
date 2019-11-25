using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server {
    public delegate  void ExecuteDelegate();
    //单线程池
    public class singleExecute {

        private static singleExecute instance = null;
         
        public static singleExecute Instance{
            get {
                lock (o) {
                    if (instance == null) {
                        instance = new singleExecute();
                    }
                    return instance;
                }
              
            }
        }
        private static object o=1;
        private Mutex mutex;
        public singleExecute() {
            mutex = new Mutex();
           
        }
        public void Execute(ExecuteDelegate executeDelegate) {
            lock (this) {
                mutex.WaitOne();
                executeDelegate();
                mutex.ReleaseMutex();
            }

        }
    }
}
