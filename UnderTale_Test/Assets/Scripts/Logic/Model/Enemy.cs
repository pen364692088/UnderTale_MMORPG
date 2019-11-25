using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy: Entity
{
 
    public List<List<AnimationCode>> inputSKill = new List<List<AnimationCode>>();
    public Dictionary<string, Component> comList = new Dictionary<string, Component>();

    public CEnemyAI AI;
    public CNavMesh Nav;

    public Vector3 outWhere = new Vector3(12.8f, -4.52f, 22.6f);
    public MonsterType mtype;


    public Enemy()
    {
        type = EntityType.Enemy;
        id = -1;
        mtype = MonsterType.Human;
        GameObject perfab = Resources.Load<GameObject>("enemy") as GameObject;
        obj = GameObject.Instantiate(perfab, Vector3.zero+ outWhere, Quaternion.identity);
       // Debug.Log("初始化敌人 Id:" + id);
        Init();
    }
    public Enemy(enemyData data,Vector3 pos)
    {
        type = EntityType.Enemy;
        SelfData = data;
        id = SelfData.id;
        mtype= data.type;
        string str = "";
        switch (mtype)
        {
            case MonsterType.Human:
                {
                    str = "enemy";
                    break;
                }
            case MonsterType.Goblin:
                {
                    str = "enemy";
                    break;
                }
        }

       // Debug.Log("初始化敌人 str:" + str);
        GameObject perfab = Resources.Load<GameObject>(str) as GameObject;
        obj = GameObject.Instantiate(perfab, pos + outWhere, Quaternion.identity);

        Init();
    }
    public void Init()
    {
       
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
        if (!obj.GetComponent<CEnemyAI>())
        {
            AI = obj.AddComponent<CEnemyAI>();
        }
        else
        {
            AI = obj.GetComponent<CEnemyAI>();
        }
        if (!obj.GetComponent<CNavMesh>())
        {
            Nav = obj.AddComponent<CNavMesh>();
        }
        else
        {
            Nav = obj.GetComponent<CNavMesh>();
        }
        comList.Add("CAnimator", ani);
        comList.Add("CSkillSimple", skill);
        comList.Add("CEnemyAI", AI);
        comList.Add("CNavMesh", Nav);
        
        foreach (var i in comList.Values)
        {
            i.owner = this;
            i.Init();
        }

      
        mySkillInfo SkillOne = new mySkillInfo(0.35f, 1.5f, 2f);

        SkillMsg.Add(AnimationCode.SkillOne, SkillOne);
        SkillMsg.Add(AnimationCode.SkillTwo, SkillOne);
        SkillMsg.Add(AnimationCode.SkillThree, SkillOne);


        //skill.SkillMsg = SkillMsg;

        skill.SetSkillMsg();
        AI.StartAI();
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
    public void setData(EntityData data)
    {
        SelfData = data;
        if (SelfData.nowHealth <= 0)
        {
            SetDead();
        }
    }

    public override void SetDead()
    {
        base.SetDead();
        
        foreach(var i in SelfData.failItem.itemList)
        {
            Vector3 pos =obj.transform.position+ new Vector3(Random.Range(0f, 1.5f), Random.Range(0f, 0.5f), Random.Range(0f, 1.5f));
            i.x = pos.x;
            i.y = pos.y;
            i.z = pos.z;
        }
        PhotonEngine.Instance.SyncItemFailing(SelfData.failItem);
    }
    public override void dataChange(int damage)
    {
       
        SelfData.beAttacked(damage);
    }
    public override void SyncData()
    {
        PhotonEngine.Instance.SyncMonsterData(SelfData);
    }
}
