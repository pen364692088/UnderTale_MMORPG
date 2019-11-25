using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UIManager:UIManagerBase
{
    public static UIManager Instance = null;
    void Awake()
    {
        Instance = this;

    }
}
