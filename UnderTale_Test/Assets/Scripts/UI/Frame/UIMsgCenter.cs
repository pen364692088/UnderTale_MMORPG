using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIMsgCenter : MonoBase
{
   

    public static UIMsgCenter Instance = null;


 
    //private socketMsg socketmsg = new socketMsg();

    private void Awake()
    {
        //  print(0);
        Instance = this;

        //    print(1);
        gameObject.AddComponent<UIManager>();
        
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
  
    public void Dispatch(AreaCode Area, int subcode, object value)
    {
        switch (Area)
        {
            case AreaCode.NET:
                {
                    PhotonEngine.Instance.Execute(subcode, value);
                    break;
                }
            case AreaCode.UI:
                {
                    UIManager.Instance.Execute(subcode, value);
                    break;
                }
          
        }
    }
   

}
