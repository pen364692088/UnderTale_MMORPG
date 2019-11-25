using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public enum AIState : byte
{
    idle=1,
    rush,
    attack,
    dead
}
public class CEnemyAI:Component
{
    public GameObject obj;
    
    private NavMeshAgent agent;
    private CSkillSimple skill;
    private CAnimator anim;
    [HideInInspector]
    public GameObject target;
    public AIState State = AIState.idle;
    private float range = 10;
    public TransData transData = new TransData();

    private List<AnimationCode> inputSkill = new List<AnimationCode>();
    public  override void Init()
    {
        obj = gameObject;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<CAnimator>();
        skill = GetComponent<CSkillSimple>();

    }
    public void StartAI()
    {
        if (PhotonEngine.Instance.isRoomMaster)
        {
            print("当前主机是房主,控制AI");
            StartCoroutine("FindPlayer");
            StartCoroutine("Moving");
            startSync();
        }
        //   print("开启协程");
    }
    public void startSync()
    {
      
            InvokeRepeating("SyncPos", 0f, 1 / 30f);
       
       
    }
    
    public void SyncPos()
    {
        transData.change(owner.id, obj.transform.position, obj.transform.forward);
        PhotonEngine.Instance.SyncMonsterTrans(transData);
        // PhotonEngine.Instance.SyncMonsterAnim(aniState.change(PhotonEngine.Instance.myId, StateDic));
        //  StateDic.Clear();
    }
    IEnumerator FindPlayer()
    {

        while (true)
        {

            yield return new WaitForSeconds(0.4f);
            if (!owner.isAlive)
            {
                State = AIState.dead;
            }
          //  print("追踪玩家");
            if (State != AIState.dead)
            {
                if (target == null)
                {
                    Collider[] colliderArr = Physics.OverlapSphere(obj.transform.position, range, LayerMask.GetMask("Player"));

                    if (colliderArr.Length > 0)
                    {
                        target = colliderArr[0].gameObject;
                      
                    }
                    else
                    {
                        target = null;
                        State = AIState.idle;
                        
                    }
                }

                if (target != null)
                {
                    float Distance = Vector3.Distance(transform.position, target.transform.position);
                    //距离判断
                    if (Distance <= 2.4f&&owner.canAttack)
                    {
                        //攻击状态
                        State = AIState.attack;
                        inputSkill.Add(AnimationCode.SkillOne);
                        skill.DoUpdataTest(inputSkill);
                        inputSkill.Clear();
                    }
                    else 
                    {
                        //追击状态
                        State = AIState.rush;
                       
                    }
                  
                }
               
            }
        }
    }
    IEnumerator Moving()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            switch (State)
            {
                case AIState.idle:
                    {
                        anim.Play(AnimationCode.Speed, 0f);
                        break;
                    }
                case AIState.rush:
                    {
                        if (target != null&&owner.canMove)
                        {
                            agent.SetDestination(target.transform.position);
                        }
                        anim.Play(AnimationCode.Speed, 1f);
                        break;
                    }
                case AIState.attack:
                    {
                        
                        agent.SetDestination(transform.position);
                        
                        break;
                    }
                case AIState.dead:
                    {
                        agent.Stop();
                        break;
                    }
            }
        }
    }


}
