using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InputData
{
    public float H=0;
    public float V=0;

    public List<AnimationCode> skillList = new List<AnimationCode>();

    public InputData()
    {
        H = 0;
        V = 0;
        skillList = new List<AnimationCode>();
    }
    public void AddKey(AnimationCode code)
    {
        skillList.Add(code);
    } 
    public void ClearKey()
    {
        skillList.Clear();
    }
    public void SetHV(string x,string y)
    {
        H = float.Parse(x);
        V = float.Parse(y);
    }
}
