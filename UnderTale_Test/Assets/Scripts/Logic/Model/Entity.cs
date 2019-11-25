using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Entity
{
    public int id;
    public EntityType type;
    public GameObject obj;
    public CAnimator ani;
    public CSkillSimple skill;

    public bool isAlive = true;

    public EntityData SelfData;
    public bool canAttack = true;
    public float Stiffness = 0.9f;
    public float StiffTimer = 0f;
    public bool canMove = true;

    public Dictionary<AnimationCode, mySkillInfo> SkillMsg = new Dictionary<AnimationCode, mySkillInfo>();

    public void beAttacked(int damage)
    {
        SelfData.health -= damage;
        canAttack = false;
        canMove = true;
        StiffTimer = Stiffness;
        if (SelfData.health <= 0)
        {
            SetDead();
        }
        else
        {
            ani.Play(AnimationCode.beAttacked);
        }
        dataChange(damage);

        UIMsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.ShowDamage, new V3Text(obj.transform.position+new Vector3(-obj.GetComponent<Collider>().bounds.size.x*0.5f, obj.GetComponent<Collider>().bounds.size.y*2,0), damage.ToString()));

        if(id== PhotonEngine.Instance.myId&&type==EntityType.Player)
        {
            UIMsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SETHP, SelfData);
        }
       
        SyncData();


    }
    public virtual void SetDead()
    {
        ani.Play(AnimationCode.isAlive, 0);
        isAlive = false;
        canMove = false;
        obj.GetComponent<Rigidbody>().Sleep();
        DestroySelf();
    }
    public virtual void SyncData()
    {

    }
    public virtual void dataChange(int damage)
    {

    }
    public virtual void DestroySelf()
    {
      
        UnityEngine.MonoBehaviour.Destroy(obj, 5f);
    }
}
