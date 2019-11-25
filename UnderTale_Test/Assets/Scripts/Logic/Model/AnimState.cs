using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class AnimState
{
    public int id = -1;
  
    public Dictionary<byte, float> dic = new Dictionary<byte, float>();
    public AnimState()
    {

    }
    public AnimState(int uid ,Dictionary<byte, float> udic)
    {
        id = uid;
        dic = udic;
    }
    public AnimState change(int uid, Dictionary<byte, float> udic)
    {
        id = uid;
        dic = udic;
        return this;
    }
  


}
