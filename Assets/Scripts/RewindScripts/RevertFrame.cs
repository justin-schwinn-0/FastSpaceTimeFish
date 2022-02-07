using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertFrame
{
    public float instanceGametime;
    public Quaternion rot;
    public Vector3 pos;

    public RevertFrame(float gt, Quaternion r, Vector3 p)
    {
        instanceGametime = gt;
        rot = r;
        pos = p;
    }

}
