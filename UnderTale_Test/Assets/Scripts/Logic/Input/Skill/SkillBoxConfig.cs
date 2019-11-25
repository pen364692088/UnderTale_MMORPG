using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillInfo")]
public class SkillBoxConfig : UnityEngine.ScriptableObject
{
    public List<SkillInfo> skillInfos = new List<SkillInfo>();

    private bool hasInit = false;

    public void CheckInit()
    {
        if (hasInit) return;
        hasInit = true;
        foreach (var info in skillInfos)
        {
            info.DoInit();
        }
    }
}
