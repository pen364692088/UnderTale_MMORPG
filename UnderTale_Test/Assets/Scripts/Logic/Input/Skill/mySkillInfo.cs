using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public class mySkillInfo
{
    
    public ESkillState state;
    public float startTime;
    public float CdTimer;
    public float doneDelay;
    public float CD;

    public mySkillInfo()
    {
        state = ESkillState.Idle;
        startTime = 0.1f;
        CD = 0;
        doneDelay = 0;
        CdTimer = 0;
    }
    public mySkillInfo(float s,float cd,float delay)
    {
        state = ESkillState.Idle;
        startTime =s;
        CD = cd;
        doneDelay = delay;
        CdTimer = 0;
    }
}
