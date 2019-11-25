using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TreasureTrigger:ScenceTrigger
{
    public GameObject go;
    private float _a = 0.0f;
    private float _b = 0.0f;
    private float jumpDuration = 0.6f; //弹起的时间
    private float height = 3; //弹起的高度
    private float _curTime = 0.0f;
    private int count = 2; //弹起的次数
    private Vector3 _homePos = Vector3.zero;
    private Vector3 _tempPos = Vector3.zero;
    public override void Start()
    {
        base.Start();
        go = gameObject;
        _homePos = go.transform.position;
        CalculateAAndB();
    }

    private void CalculateAAndB()
    {
        _a = -4 * height / Mathf.Pow(jumpDuration, 2);
        _b = _a * (-1) * jumpDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject)
        {
            if (_curTime < jumpDuration)
            {
                _curTime += Time.deltaTime;
                _tempPos = _homePos + (_a * Mathf.Pow(_curTime, 2) + _b * _curTime) * Vector3.up;
                go.transform.position = _tempPos;
            }
            else if (count > 0)
            {
                count = count - 1;
                _curTime = 0;
                height = 0.5f * height;
                jumpDuration = 0.5f * jumpDuration;
                CalculateAAndB();
            }
        }
    }
    public override void beAttached()
    {
        ItemManager.Instance.PeopleGetThing(this.gameObject);
       
    }
    public override void beTaked()
    {
        beCatched();
    }
    public void beCatched()
    {
        print("被捡起来了");
        Destroy(this.gameObject);
    }
    public override void Collsion()
    {
       // base.Collsion();
    }
}
