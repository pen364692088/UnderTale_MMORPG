using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class CNavMesh:Component
{
    private GameObject obj;
    private NavMeshAgent agent;
    [HideInInspector]
    public Entity target;
    // Start is called before the first frame update
    public  override void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        obj = gameObject;
       // rushToPos(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 150))
        //    {
        //        if (hit.transform.tag == "Floor")
        //        {
        //            agent.SetDestination(hit.point);
        //        }

        //    };
        //}
        
    }

   public  void rushToPos(Vector3 pos)
    {
        agent.SetDestination(pos + new Vector3(obj.transform.position.x - pos.x,0, obj.transform.position.z - pos.z).normalized*2);
    }

    public void rushToObj(GameObject obj)
    {
        agent.SetDestination(obj.transform.position);
    }

}
