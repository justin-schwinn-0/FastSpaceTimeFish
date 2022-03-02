using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpin : MonoBehaviour
{
    public float Speed = 360f;
    private Vector3 rot = new Vector3();
    
    
    // Update is called once per frame
    void Update()
    {
        rot.y = Speed * SpecialTime.DeltaTime();

        transform.Rotate(rot);
    }
}

