
using System;
using System.Collections.Generic;
using UnityEngine;


public class UIManagerBase : MonoBase
{
    protected Dictionary<int, List<MonoBase>> dict = new Dictionary<int, List<MonoBase>>();

    public override void Execute(int eventCode, object value)
    {
        if (!dict.ContainsKey(eventCode))
        {
            print("该消息码未注册 :" + eventCode);
            return;
        }
        List<MonoBase> list = dict[eventCode];
        foreach (var i in list)
        {
            i.Execute(eventCode, value);
        }
    }

    public void Add(int eventCode, MonoBase mono)
    {
        List<MonoBase> list = null;
        if (!dict.ContainsKey(eventCode))
        {
            list = new List<MonoBase>();
            list.Add(mono);
            dict.Add(eventCode, list);
        }
        else
        {
            list = dict[eventCode];
            list.Add(mono);
        }
    }

    public void Add(int[] eventCode, MonoBase mono)
    {
        foreach (var i in eventCode)
        {
            Add(i, mono);
        }
    }
    public void Remove(int eventCode, MonoBase mono)
    {
        List<MonoBase> list = null;
        if (!dict.ContainsKey(eventCode))
        {
            print("该消息码未注册 :" + eventCode + " 无法注销");
            return;
        }
        list = dict[eventCode];
        if (list.Count == 1)
        {
            dict.Remove(eventCode);
        }
        else
        {
            list.Remove(mono);
        }
    }
    public void Remove(int[] eventCode, MonoBase mono)
    {
        foreach (var i in eventCode)
        {
            Remove(i, mono);
        }
    }

}
