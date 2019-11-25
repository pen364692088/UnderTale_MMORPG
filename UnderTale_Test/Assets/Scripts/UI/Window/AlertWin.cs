using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FairyGUI;
public class AlertWin : Window
{
    private GTextField title = null;
    public AlertWin()
    {

    }
    protected override void OnInit()
    {
        base.OnInit();

        this.contentPane = UIPackage.CreateObject("TestPag", "alertMsgWin").asCom;
        UIConfig.modalLayerColor = new Color(88, 88, 88);
        title = this.contentPane.GetChild("frame").asCom.GetChild("title").asTextField;
        //  this

    }
    public void changeTitle(string str)
    {
        title.text = str;
    }
}
