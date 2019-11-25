using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Component
{
    private GameObject obj;
    public bool isSelf=false;
    public bool isOffLine = false;
    public float speed = 3;
    public float TurnSpeed = 3;
    private bool isPC =>GameManager.Instance.isPC;
    private Vector3 tempForward = new Vector3();
    [HideInInspector]
    public CAnimator ani;
    public TransData transData = new TransData();

    public InputData data ;

    public override void Init()
    {
        obj = gameObject;
       // isPC = GameManager.Instance.isPC;
        if (!ani)
        {
            ani = obj.GetComponent<CAnimator>();
        }
      
    }

   
    void FixedUpdate()
    {
        if (isSelf)
        {
            if (isPC)
            {
                if (isOffLine)
                {
                    if (obj)
                    {
                        InputKey_offLine();
                    }
                    else
                    {
                        Init();
                    }

                }
                else
                {
                    InputPCKey();
                }
            }
            else {
                InputMobleKey();
            }
           
            
        }
    }
    private void InputKey_offLine()
    {
      //  skillList.Clear();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float z = 0;
        if (Input.GetKeyDown("k"))
        {
           // skillList.Add(AnimationCode.SkillOne);
        }
        if (Input.GetKeyDown("e"))
        {
            TriggerScenceObject();
        }

        if ((h != 0 || v != 0)&&owner.isAlive)
        {
                obj.transform.position += Vector3.forward * v * Time.deltaTime * speed + Vector3.right * h * Time.deltaTime * speed;
                tempForward = Vector3.Slerp(obj.transform.forward, Vector3.forward * v + Vector3.right * h, TurnSpeed * Time.deltaTime);
                obj.transform.forward = tempForward;
                z = 1;
        }



        ani.Play(AnimationCode.Speed, z);
     //   (owner as Player).inputSKill.Add(skillList);
     //   gameObject.GetComponent<CSkillSimple>().DoUpdataTest(skillList);
      //  skillList.Clear();
    }
    private void InputMobleKey()
    {
        data = GameManager.Instance.input;
        float h =  data.H;
        float v = data.V;
        float z = 0;
        //print("H" + h + "V" + v);

        if (data.skillList.Contains(AnimationCode.SkillThree) && PhotonEngine.Instance.isRoomMaster)
        {
            EntityManager.Instance.SpawnerEnemy();
        }
        if (data.skillList.Contains(AnimationCode.SkillTwo))
        {
            TriggerScenceObject();
        }
        if (h != 0 || v != 0)

        {
            if (owner.canMove)
            {
                obj.transform.position += Vector3.forward * v * Time.deltaTime * speed + Vector3.right * h * Time.deltaTime * speed;
                tempForward = Vector3.Slerp(obj.transform.forward, Vector3.forward * v + Vector3.right * h, TurnSpeed * Time.deltaTime);
                obj.transform.forward = tempForward;
                z = 1;
            }
            else
            {
                print("硬直不能移动");
            }
        }



        ani.Play(AnimationCode.Speed, z);

        gameObject.GetComponent<CSkillSimple>().DoUpdataTest(data.skillList);
        data.ClearKey();
    }
    private void InputPCKey()
    {
        //skillList.Clear();
        data = GameManager.Instance.input;
        data.SetHV(Input.GetAxis("Horizontal").ToString("0.00"), Input.GetAxis("Vertical").ToString("0.00"));

        float h = data.H;
        float v = data.V;
        float z = 0;
        //print("H" + h + "V" + v);
        if (Input.GetKeyDown("k"))
        {
            data.AddKey(AnimationCode.SkillOne);
        }

        if (Input.GetKeyDown("p")&&PhotonEngine.Instance.isRoomMaster)
        {
            EntityManager.Instance.SpawnerEnemy();
        }
        if (Input.GetKeyDown("e"))
        {
            TriggerScenceObject();
        }
        if (h != 0 || v != 0)
      
        {
            if (owner.canMove)
            {
                obj.transform.position += Vector3.forward * v * Time.deltaTime * speed + Vector3.right * h * Time.deltaTime * speed;
                tempForward = Vector3.Slerp(obj.transform.forward, Vector3.forward * v + Vector3.right * h, TurnSpeed * Time.deltaTime);
                obj.transform.forward = tempForward;
                z = 1;
            }
            else
            {
               // print("硬直不能移动");
            }
        }
       
      
        
        ani.Play(AnimationCode.Speed, z);
        
        gameObject.GetComponent<CSkillSimple>().DoUpdataTest(data.skillList);
        data.ClearKey();
    }
    public void startSync()
    {
        if (isSelf)
        {
            InvokeRepeating("SyncPos", 0f, 1 / 30f);
        }
    }
    private void SyncPos()
    {
        transData.change(PhotonEngine.Instance.myId, obj.transform.position, obj.transform.forward);
        PhotonEngine.Instance.SyncPos(transData);
    }
    public void TriggerScenceObject()
    {
        Collsion(new Vector3(0, obj.GetComponent<Collider>().bounds.center.y, 0), 2.5f);
    }
    public void Collsion(Vector3 hight,float rang)
    {

        Collider[] colliderArr = Physics.OverlapSphere(obj.transform.position + hight, rang, LayerMask.GetMask("ScenceObject"));
        
        for (int i = 0; i < colliderArr.Length; i++)
        {
            Vector3 v3 = colliderArr[i].gameObject.transform.position - obj.transform.position;
            float angle = Vector3.Angle(v3, obj.transform.forward);
            colliderArr[i].GetComponent<ScenceTrigger>().beAttached();
            //print("场景互动");
            break;
        }

       

    }
}
