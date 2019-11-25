using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CAnimator:Component
 {
    [HideInInspector]
    public GameObject obj;
    public Animator anim;
    public Dictionary<byte, float> StateDic = new Dictionary<byte, float>();
    public AnimState aniState = new AnimState();

    
    public override void Init()
    {
        obj = gameObject;
        anim = obj.GetComponent<Animator>();
        
       
    }
    public void startSync()
    {
        InvokeRepeating("SyncAnim", 0f, 1 / 10f);
    }
    public void SyncAnim()
    {
        if (owner.type == EntityType.Player&& owner.id==PhotonEngine.Instance.myId)
        {
           // print("发送自己的anim");
            PhotonEngine.Instance.SyncAnim(aniState.change(PhotonEngine.Instance.myId, StateDic));
        }else if (owner.type == EntityType.Enemy && PhotonEngine.Instance.isRoomMaster)
        {
           // print("发送敌人的anim");
            PhotonEngine.Instance.SyncMonsterAnim(aniState.change(owner.id, StateDic));
        }
        StateDic.Clear();
    }
    public void Play(AnimationCode code,float value=0)
    {
        anim.SetFloat("Speed", 0);
        switch (code)
        {
            case AnimationCode.Speed:
                {
                   
                    anim.SetFloat("Speed", value);
                    
                    break;
                }
            case AnimationCode.SkillOne:
                {
                    anim.SetTrigger("SkillOne");
                   
                    break;
                }
            case AnimationCode.beAttacked:
                {
                    anim.SetTrigger("beAttacked");
                    break;
                }
            case AnimationCode.isAlive:
                {
                    anim.SetBool("isAlive", value == 0 ? false : true);
                    break;
                }
          
        }
        if (StateDic.ContainsKey((byte)code))
        {
            StateDic[(byte)code] = value;
        }
        else
        {
            StateDic.Add((byte)code, value);
        }

        if (code!= AnimationCode.Speed)
        {

            SyncAnim();
        }
        
    }
   
    public void Play(Dictionary<byte,float> dic)
    {
        if (anim == null)
        {
            anim = gameObject.GetComponent<Animator>();
        }
        foreach (var code in dic.Keys)
        {
            float value = (float)dic[code];
            
            //print("code:" + code + " " + value.ToString());
            
            switch (code)
            {
                case (byte)AnimationCode.Speed:
                    {
                        
                        anim.SetFloat("Speed", value);

                        break;
                    }
                case (byte)AnimationCode.SkillOne:
                    {
                        anim.SetTrigger("SkillOne");
                        
                        break;
                    }
                case (byte)AnimationCode.beAttacked:
                    {
                        anim.SetTrigger("beAttacked");
                        break;
                    }
                case (byte)AnimationCode.isAlive:
                    {
                        anim.SetBool("isAlive", value == 0 ? false : true);
                        break;
                    }
            }
        }
       
       
     

    }
}
