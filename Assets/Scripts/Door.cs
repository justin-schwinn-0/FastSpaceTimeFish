using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public FTGreen Activator;

    public bool opensOut;
    public bool HingeOnLeft;

    public Transform Hinge;

    void Start()
    {
        float back = transform.position.z + transform.lossyScale.z/2;
        float front = transform.position.z - transform.lossyScale.z/2;

        float left = transform.position.x + transform.lossyScale.x/2;
        float right = transform.position.x - transform.lossyScale.x/2;

        Vector3 pos = new Vector3();
        pos.z = (opensOut) ? back:front;
        pos.x = (HingeOnLeft)? left : right;

        Hinge.transform.SetPositionAndRotation(pos, transform.rotation);
    }

    public void Update()
    {
        if(Activator.isActive())
        {

        }
    }
}
