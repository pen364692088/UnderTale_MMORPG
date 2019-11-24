using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Util.Current {

    //线程安全 Int类型
   public  class CurrentInt {
       private int value;
       //lock只能锁对象object
       public CurrentInt(int v) {
           this.value = v;
       }
       public int  Add_Get(){
           lock (this)
	        {
               
		        value++;
               return value;
	        }
       }
       public int Reduce_Get() {
           lock (this) {

               value--;
               return value;
           }
       }
       public int Get() {
          
               return value;
           
       }
    }
}
