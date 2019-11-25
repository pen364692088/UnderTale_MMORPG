using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class SkillColliderInfo
{
    public Vector3 pos;
    public Vector2 size;
    public float radius;
    public float deg = 180;
    public float maxY;

    public bool IsCircle => radius > 0;
}

[Serializable]
public class SkillPart
{
    public bool _DebugShow;
    public float startTimer;
    public SkillColliderInfo collider;
    public Vector3 impulseForce;
    public bool needForce;
    public bool isResetForce;

    public float interval;
    public int otherCount;
    public int damage;
    public static float AnimFrameScale =0.166f;
    [HideInInspector] public float DeadTimer => startTimer + interval * (otherCount );

    [HideInInspector] public int counter;

    public float NextTriggerTimer()
    {
        return startTimer + interval * counter;
    }
}

[Serializable]
public class SkillInfo
{
    public AnimationCode animName;
    public float CD;
    public float doneDelay;
    public int targetLayer;
    public float maxPartTime;
    public List<SkillPart> parts = new List<SkillPart>();

    public void DoInit()
    {
        parts.Sort((a, b) => Math.Sign(a.startTimer - b.startTimer));
        var time = float.MinValue;
        foreach (var part in parts)
        {
            part.startTimer = part.startTimer * SkillPart.AnimFrameScale;
            var partDeadTime = part.DeadTimer;
            if (partDeadTime > time)
            {
                time = partDeadTime;
            }
        }

        maxPartTime = time + doneDelay;
    }
}
