using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestTrigger:ScenceTrigger
{
    private Queue<ItemData> treasuresList=new Queue<ItemData>();
    public override void Start()
    {
        base.Start();
       for(int i = 0; i < 5; i++)
        {
            ItemData item = new ItemData();
            item.type = ItemType.Gold;
            item.num = 1;
            Vector3 pos = transform.position + new Vector3(Random.Range(0f, 1.5f), Random.Range(0f, 0.5f), Random.Range(0f, 1.5f));
            item.x = pos.x;
            item.y = pos.y;
            item.z = pos.z;
           treasuresList.Enqueue(item);
        }
      
     
        
        
    }
    public override void beAttached()
    {
        //print("被触发");
        PhotonEngine.Instance.SyncItemFailing(new ItemPackage (treasuresList));
        ItemManager.Instance.PeopleGetThing(this.gameObject);
        //  this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
   


}
