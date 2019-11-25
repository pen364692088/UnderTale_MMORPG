using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityManager: ManagerBase
{
    private static EntityManager _instance;
    public static EntityManager Instance {
        get {
            if (_instance == null)
            {
                _instance = new EntityManager();
            }
            return _instance;
        }
    }

    public  Dictionary<int, Player> idPlayerDic = new Dictionary<int, Player>();
    public  Dictionary<int, Enemy> idEnemyDic = new Dictionary<int, Enemy>();

    //public Dictionary<int, Player> idPlayerDic = new Dictionary<int, Player>();

    public Timer m_Timer;

    public void Awake()
    {
        _instance = this;
    }
    public void InitWorld()
    {
        InitPlayer();
        InitEnemy();
       
    }
    public void InitPlayer()
    {
      
        foreach (var user in PhotonEngine.Instance.world.userList)
        {
            AddPlayer(user);
           
        }
        GameManager.Instance.SelfPlayer = idPlayerDic[PhotonEngine.Instance.myId];
        PlayerCtrl pc = idPlayerDic[PhotonEngine.Instance.myId].obj.GetComponent<PlayerCtrl>();
        CAnimator ca = idPlayerDic[PhotonEngine.Instance.myId].obj.GetComponent<CAnimator>();

        pc.isSelf = true;

        GameManager.Instance.playerIsAlive = true;

        pc.startSync();

        ca.startSync();

        GameManager.Instance.Look(idPlayerDic[PhotonEngine.Instance.myId].obj);

     

     //   UIMsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SETHP, GameManager.Instance.SelfPlayer.SelfData);

       
    }
    public void InitEnemy()
    {
        foreach (var enemy in PhotonEngine.Instance.world.enemyList)
        {
            AddEnemy(enemy);
        }
        //System.Timers.Timer timer1 = new System.Timers.Timer();
        //timer1.Elapsed += new System.Timers.ElapsedEventHandler((obj, eventArg) => {
        //    SpawnerEnemy();
        //    Debug.Log("产生敌人");
        //});
        //timer1.Interval = 2000;//毫秒 1秒=1000毫秒
        //timer1.Enabled = true;
        //timer1.AutoReset = false;//执行一次 false，一直执行true 

       
      //  m_Timer = new Timer(new TimerCallback(SpawnerEnemy), this, 2000, 0);
    }
    public void SpawnerEnemy()
    {
        Debug.Log("添加敌人");
        enemyData data = null;
        if (PhotonEngine.Instance.world.enemyList.Count > 0)
        {
             data = new enemyData(PhotonEngine.Instance.world.enemyList[PhotonEngine.Instance.world.enemyList.Count - 1].id + 1, MonsterType.Human);
        }
        else
        {
             data = new enemyData(1, MonsterType.Human);
        }
      
        PhotonEngine.Instance.world.enemyList.Add(data);
        AddEnemy(data);

        PhotonEngine.Instance.AddEnemy(data);
    }
  
    public void AddPlayer(RolerData data)
    {
        if (!PhotonEngine.Instance.world.userList.Contains(data))
        {
            PhotonEngine.Instance.world.userList.Add(data);
        }
        idPlayerDic.Add(data.id, new Player(data));

    }
    public void AddEnemy(enemyData data)
    {
        if (!PhotonEngine.Instance.world.enemyList.Contains(data))
        {
            PhotonEngine.Instance.world.enemyList.Add(data);
        }
        Debug.Log("AddEnemy" + data.type);
      
        idEnemyDic.Add(data.id, new Enemy(data, randowPos()));

       
       // idPlayerDic.Add(data.id, new Player(data.id, randowPos()));
    }
    public void RemoveEnemy(enemyData data)
    {
        if (PhotonEngine.Instance.world.enemyList.Contains(data))
        {
            PhotonEngine.Instance.world.enemyList.Remove(data);
        }
        GameObject.Destroy(idEnemyDic[data.id].obj);
        idEnemyDic.Remove(data.id);
    }
    public void RemovePlayer(RolerData data)
    {
        if (PhotonEngine.Instance.world.userList.Contains(data))
        {
            PhotonEngine.Instance.world.userList.Remove(data);
        }
        GameObject.Destroy(idPlayerDic[data.id].obj);
        idPlayerDic.Remove(data.id);
    }
    public void syncTrans(TransData data)
    {
        Player player = null;
        idPlayerDic.TryGetValue(data.id, out player);
        if (player != null)
        {
            player.setPos(data);
        }
        else
        {
            Debug.Log("位置同步出错 不存在玩家 id:" + data.id);
        }
    }
    public void syncAnim(AnimState data)
    {
        Player player = null;
        idPlayerDic.TryGetValue(data.id, out player);
        if (player != null)
        {
            player.setAnim(data);
        }
        else
        {
            Debug.Log("动画同步出错 不存在玩家 id:" + data.id);
        }
    }

    public void syncMonstTrans(TransData data)
    {
        Enemy enemy = null;
        idEnemyDic.TryGetValue(data.id, out enemy);
        if (enemy != null)
        {
            enemy.setPos(data);
        }
        else
        {
            Debug.Log("位置同步出错 不存在玩家 id:" + data.id);
        }
    }
    public void syncMonstAnim(AnimState data)
    {
        Enemy enemy = null;
        idEnemyDic.TryGetValue(data.id, out enemy);
        if (enemy != null)
        {
            enemy.setAnim(data);
        }
        else
        {
            Debug.Log("动画同步出错 不存在怪物 id:" + data.id);
        }
    }
    public void syncMonstData(EntityData data)
    {
        Enemy obj = null;
        idEnemyDic.TryGetValue(data.id, out obj);
        if (obj != null)
        {
            obj.setData(data);
        }
        else
        {
            Debug.Log("数据同步出错 不存在怪物 id:" + data.id);
        }
    }
    public void syncPlayerData(EntityData data)
    {
        Player obj = null;
        idPlayerDic.TryGetValue(data.id, out obj);
        if (obj != null)
        {
            obj.setData(data);
        }
        else
        {
            Debug.Log("数据同步出错 不存在角色 id:" + data.id);
        }
    }
    public Vector3 randowPos()
    {

        return new Vector3( Random.Range(-3,3),0, Random.Range(-3, 3));
    }
   
}
