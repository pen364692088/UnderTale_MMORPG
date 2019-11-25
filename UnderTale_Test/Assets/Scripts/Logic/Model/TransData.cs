using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class TransData
{
    public int id;
    public double px;
    public double py;
    public double pz;

    public double fx;
    public double fy;
    public double fz;

    public TransData()
    {

    }
    public TransData(int uid,Vector3 pos,Vector3 forward)
    {
        id = uid;
        px = pos.x;
        py = pos.y;
        pz = pos.z;

        fx = forward.x;
        fy = forward.y;
        fz = forward.z;

    }
    public void change(int uid, Vector3 pos, Vector3 forward)
    {
        id = uid;
        px = pos.x;
        py = pos.y;
        pz = pos.z;

        fx = forward.x;
        fy = forward.y;
        fz = forward.z;

    }
    public Vector3 getPos()
    {
        return new Vector3((float)px, (float)py, (float)pz);
    }
    public Vector3 getRoation()
    {
        return new Vector3((float)fx, (float)fy, (float)fz);
    }
}
