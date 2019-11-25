using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Player:Entity
{
  
    [HideInInspector]
    public PlayerCtrl input;
    [HideInInspector]
    public List<List<AnimationCode>> inputSKill=new List<List<AnimationCode>>();
    [HideInInspector]
    public Dictionary<string, Component> comList = new Dictionary<string, Component>();
    public Player()
    {
        type = EntityType.Player;
        id = -1;
        GameObject perfab = Resources.Load<GameObject>("Player_Knight") as GameObject;
        obj = GameObject.Instantiate(perfab, Vector3.zero, Quaternion.identity);
        Init();
    }
    public Player(int uid,Vector3 pos)
    {
        type = EntityType.Player;
        id = uid;
        GameObject perfab = Resources.Load<GameObject>("Player_Knight") as GameObject;
        obj = GameObject.Instantiate(perfab, pos, Quaternion.identity);
        Init();
    }
    public Player(RolerData data)
    {
        SelfData = data;
        type = EntityType.Player;
        id = data.id;
        GameObject perfab = Resources.Load<GameObject>("Player_Knight") as GameObject;
        obj = GameObject.Instantiate(perfab, new Vector3(data.x,data.y,data.z), Quaternion.identity);
        Init();

        
    }
    public void Init()
    {
        if (!obj.GetComponent<PlayerCtrl>())
        {
            input = obj.AddComponent<PlayerCtrl>();
        }
        else
        {
            input = obj.GetComponent<PlayerCtrl>();
        }

        if (!obj.GetComponent<CAnimator>())
        {
            ani = obj.AddComponent<CAnimator>();
        }
        else
        {
            ani = obj.GetComponent<CAnimator>();
        }
        if (!obj.GetComponent<CSkillSimple>())
        {
            skill = obj.AddComponent<CSkillSimple>();
        }
        else
        {
            skill = obj.GetComponent<CSkillSimple>();
        }
      
        comList.Add("PlayerCtrl", input);
        comList.Add("CAnimator", ani);
        comList.Add("CSkillSimple", skill);


        foreach (var i in comList.Values)
        {
            i.owner = this;
            i.Init();
        }

        input.ani = ani;


        mySkillInfo SkillOne = new mySkillInfo(0.35f, 1.5f, 10f);

        SkillMsg.Add(AnimationCode.SkillOne, SkillOne);
        SkillMsg.Add(AnimationCode.SkillTwo, SkillOne);
        SkillMsg.Add(AnimationCode.SkillThree, SkillOne);


        //skill.SkillMsg = SkillMsg;

        skill.SetSkillMsg();

    }
    public void setPos(TransData data)
    {
        obj.transform.position = data.getPos();
        obj.transform.forward = data.getRoation();
    }
    public void setAnim(AnimState data)
    {
        ani.Play(data.dic);
    }
    public override void SyncData()
    {
        PhotonEngine.Instance.SyncPlayerData(SelfData);
    }
    public void setData(EntityData data)
    {
        SelfData = data;
        if (SelfData.nowHealth <= 0)
        {
            SetDead();
            if (id == PhotonEngine.Instance.myId)
            {
                GameManager.Instance.playerIsAlive = false;
            }
        }
    }
    public override void dataChange(int damage)
    {
        base.dataChange(damage);
        if (id == PhotonEngine.Instance.myId)
        {
           // CameraManager.Instance.beAttackedEffect();
        }
    }
}
