using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ISkillEventHandler
{
    void OnSkillStart(Skill skill);
    void OnSkillDone(Skill skill);
    void OnSkillPartStart(Skill skill);
}

public enum ESkillState
{
    Idle,
    Firing,
}
public class CSkillSimple:Component
{
   

    private float range =3.6f;
    public GameObject obj;
    public CAnimator anim;
    public Vector3 hight;

    //private int Health = 1000;

    //public bool isAlive = true;
   

    public PlayerCtrl ctrl=null;
    public string attackLayMask = "Enemy";
    public ISkillEventHandler eventHandler;

    public GameObject WeaponPos;
    private GameObject effectTest;

    private Dictionary<AnimationCode, mySkillInfo> SkillMsg = new Dictionary<AnimationCode, mySkillInfo>();
    public override void Init()
    {
        obj = gameObject;
        anim = GetComponent<CAnimator>();
        hight = new Vector3(0, obj.GetComponent<Collider>().bounds.center.y,0);
        // print(hight);
        effectTest = WeaponPos.transform.Find("weaponFx").gameObject;

        if (owner.type==EntityType.Enemy)
        {
            attackLayMask = "Player";
        }
        else
        {
            ctrl = GetComponent<PlayerCtrl>();
        }
        
        //mySkillInfo SkillOne = new mySkillInfo(0.35f, 1.5f,2f);

        //SkillMsg.Add(AnimationCode.SkillOne, SkillOne);
        //SkillMsg.Add(AnimationCode.SkillTwo, SkillOne);
        //SkillMsg.Add(AnimationCode.SkillThree, SkillOne);
    }
    public void SetSkillMsg()
    {
        
        SkillMsg = owner.SkillMsg; 
    }
    public bool ifShowDraw = false;
    public void Collsion(int damage,float rang,float maxangle)
    {
        if (!owner.canAttack || !owner.isAlive)
        {
            return;
        }
        Collider[] colliderArr = Physics.OverlapSphere(obj.transform.position + hight, rang, LayerMask.GetMask(attackLayMask));
      
        ifShowDraw = true;
        for (int i = 0; i < colliderArr.Length; i++)
        {
            Vector3 v3 = colliderArr[i].gameObject.transform.position - obj.transform.position;
            float angle = Vector3.Angle(v3, obj.transform.forward);
            if (angle < maxangle)
            {
          //      print("攻击到了");
                colliderArr[i].gameObject.GetComponent<CSkillSimple>().owner.beAttacked(damage);
                // 距离和角度条件都满足了
            }
        }
        Invoke("CloseDraw", 0.3f);
    }
    public  void AttackEvent(int type)
    {
        switch (type){
            case 1:{
                    Collsion(30, 4, 130);
                    break;
            }
            case 2:{
                    Collsion(40,4, 150); 
                break;
            }
            case 3:
                {
                    Collsion(20,4.4f, 170);

                    break;
            }
        }
        PlayEffect(effectTest);
      //  print(type);
      
    }
    
    
    public void CloseDraw()
    {
        ifShowDraw = false;
    }
    public void OnDrawGizmos()
    {
#if UNITY_EDITOR 
            float tintVal = 0.3f;
            Gizmos.color = new Color(0, 1.0f - tintVal, tintVal, 0.25f);

        if (ifShowDraw)
        {
            Gizmos.DrawSphere(obj.transform.position + hight, range);
        }
        else
        {
            return;
        }
         

       
#endif
    }
    void Update()
    {
        owner.StiffTimer -= Time.deltaTime;
        if (owner.StiffTimer <= 0&& !owner.canAttack)
        {
            owner.canAttack = true;
        }
      
        
        foreach (mySkillInfo skill in SkillMsg.Values)
        {
            skill.doneDelay -= Time.deltaTime;
            skill.CdTimer-= Time.deltaTime;
            if (skill.CdTimer<=0)
            {
                skill.state = ESkillState.Idle;
            }
            
           
        }
    }
    //public void startCollsion()
    //{
    //    Collsion();
    //}
    public void PlayEffect(GameObject obj)
    {
        if (!obj.GetComponent<ParticleSystem>().isPlaying)
        {
            obj.GetComponent<ParticleSystem>().Play();
        }

    }
    public void SkillStart(int skillId)
    {
        owner.canMove = false;
    }
    public void SkillEnd(int skillId)
    {
        owner.canMove = true;
    }
    public void DoUpdataTest(List<AnimationCode> inputSkill)
    {
        if (!owner.canAttack || !owner.isAlive)
        {
            return;
        }
        foreach (var code in inputSkill)
            {
                if (SkillMsg.ContainsKey(code))
                {
                    if (SkillMsg[code].doneDelay <= 0)
                    {
                        mySkillInfo temp = SkillMsg[code];
                        temp.doneDelay = temp.CD;
                       
                        anim.Play(code);

                       
                        
                    }
                    else
                    {
                        print("该技能在CD中");
                    }

                }
                else
                {
                    print("碰撞检测无skillID:" + code);
                }

            }
        
      
    }
   

    
}
