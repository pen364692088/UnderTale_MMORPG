using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ScenceTrigger:MonoBehaviour
{
    public float rang = 2f;
   
    public float maxangle = 130;
    [HideInInspector]
    public GameObject tip;
    [HideInInspector]
    public GameObject obj;
    [HideInInspector]
    public Vector3 hight = new Vector3();
    public bool show = false;
    public void Awake()
    {
        
        obj = gameObject;
        tip = Resources.Load<GameObject>("TipPos") as GameObject;
      
        hight = new Vector3(0, obj.GetComponent<Collider>().bounds.center.y, 0);
        tip= GameObject.Instantiate(tip, transform.position + hight+new Vector3(0,1.5f,0), Quaternion.Euler(45,0,0), transform);
    }
    public virtual void Start()
    {
        StartCoroutine("FindPlayer");
    }
    public IEnumerator FindPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            Collsion();
        }
    }
    public void TipTrigger(bool show)
    {
        if (tip.activeSelf == show)
        {
            return;
        }
        tip.SetActive(show);
    }
    public virtual void Collsion()
    {
      
        Collider[] colliderArr = Physics.OverlapSphere(obj.transform.position + hight, rang, LayerMask.GetMask("Player"));

        show = false;
        for (int i = 0; i < colliderArr.Length; i++)
        {
            Vector3 v3 = colliderArr[i].gameObject.transform.position - obj.transform.position;
            float angle = Vector3.Angle(v3, obj.transform.forward);
          
            show = true;
            print("玩家靠近");
                // colliderArr[i].gameObject.GetComponent<CSkillSimple>().owner.beAttacked(damage);
                // 距离和角度条件都满足了
            
        }

        TipTrigger(show);
      
    }
    public void OnDrawGizmos()
    {

        //float tintVal = 0.3f;
         Gizmos.color = new Color(0,0.4f, 0.4f, 0.25f);
       
         Gizmos.DrawSphere(obj.transform.position + hight, rang);
    }
    public virtual void beAttached()
    {
        print("触发");
    }
    public virtual void beTaked()
    {
        Destroy(this.gameObject);
    }
}
