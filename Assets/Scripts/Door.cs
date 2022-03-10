using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public FTGreen Activator;
    public Transform OpenPosition;
    public Transform ClosedPosition;
    public Transform DoorBlock;
    void Start()
    {
        
    }

    public void Update()
    {
        if(Activator.isActive())
        {
            DoorBlock.position = OpenPosition.position;
        }
        else DoorBlock.position = ClosedPosition.position;
    }
}
