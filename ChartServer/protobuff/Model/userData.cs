using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class userData
    {
    public int id;
    public bool isOnline=false;
    public string username;
    public string password;
    public List<RolerData> roleData=new List<RolerData>();
    public userData()
    {

    }
    public userData(int uid, string user,string psw)
    {
        id = uid;
        username = user;
        password = psw;
        roleData = new List<RolerData>();
    }
    public void OnLine()
    {
        isOnline = true;
    }
    public void OffLine()
    {
        isOnline = false;
    }
    public void RegistRole(Dictionary<string,object>dic)
    {
        roleData.Add(new RolerData(id, roleData.Count, dic));
    }
}
