using protobuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class ParameterTools
{
    public static object GetSubCode(Dictionary<byte, object> dic)
    {
        return dic[(byte)ParameterCode.SubCode];
    }
    public static object GetReturnCode(Dictionary<byte, object> dic)
    {
        return dic[(byte)ParameterCode.ReturnCode];
    }
    public static void AddData(Dictionary<byte, object> dic,ParameterCode code,object data,bool isObj)
    {
        if (isObj)
        {
            dic.Add((byte)code, JsonConvert.SerializeObject(data));
        }
        else
        {
            dic.Add((byte)code, data);
        }
        
    }
    public static T GetData<T>(Dictionary<byte, object> dic, ParameterCode code,bool isObj)
    {
        object o = null;
        dic.TryGetValue((byte)code, out o);
        if (isObj == false)
        {
            return (T)o;
        }
        return JsonConvert.DeserializeObject<T>(o.ToString());
      
    }
    public static  void SetSubCode(Dictionary<byte, object> dic, SubCode subCode)
    {
        dic.Add((byte)ParameterCode.SubCode, subCode);
    }
    public static void SetReturnCode(Dictionary<byte, object> dic, ReturnCode returnCode)
    {
        dic.Add((byte)ParameterCode.ReturnCode, returnCode);
    }

}
