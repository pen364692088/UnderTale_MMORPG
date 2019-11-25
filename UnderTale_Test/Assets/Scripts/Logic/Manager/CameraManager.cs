using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraManager: ManagerBase
{
    private static CameraManager _instance;
    private   bool startFollow = false;
    private   Transform target;

    private float alpha = 0.4f;
    private float nowAlpha = 0.4f;
    private  float flash_Speed = 3;
    public static CameraManager Instance {
        get {
            if (_instance == null)
            {
                _instance = new CameraManager();
            }
            return _instance;
        }
    }
    private  GameObject mycamera;
    public void Awake()
    {
        _instance = this;
    }

    public  override void seflUpdate()
    {
       
        if (startFollow&&GameManager.Instance.playerIsAlive)
        {

            if (Vector3.Distance(mycamera.transform.position, target.position) > 1)
            {
                mycamera.transform.position = Vector3.Lerp(mycamera.transform.position, new Vector3(target.position.x, mycamera.transform.position.y, target.position.z - 55), 0.3f);
            }
        }
       
            //print("nowAlpha" + nowAlpha);
            //nowAlpha=Mathf.Lerp(nowAlpha, 0, flash_Speed*Time.deltaTime);
            //beAttacked_Image.color = new Color(0, 0, 0, nowAlpha);
           
            //if (beAttacked_Image.color.a< 0.05f)
            //{
            //    beAttacked_Image.color = Color.clear;
            //    beAttacked = false;
            //}
        


    }
    public void Look(GameObject obj)
    {
        // mycamera.transform.position=obj.transform.Find("cameraPos").position;
        //print(obj);
        

        initCamera();
        target = obj.transform;
        mycamera.transform.position = new Vector3(target.position.x, mycamera.transform.position.y, target.position.z -55 );


        startFollow = true;
     

    }
    public void initCamera()
    {
        mycamera = GameObject.FindGameObjectWithTag("MainCamera");
      //  beAttacked_Image = GameObject.FindGameObjectWithTag("EffectUI").GetComponent<Image>();
    }
    public void beAttackedEffect()
    {
        //beAttacked_Image.color = new Color(0,0,0,alpha);
        //nowAlpha = alpha;


        //if (!beAttacked)
        //{
        //    beAttacked = true;
          
        //}
    }
  
}
