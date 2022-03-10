using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretAimState
{
    Aiming, Firing
}

public class TurretAim : MonoBehaviour
{
    public Transform player;
    
    public Transform turretBody;
    public Transform gunAnchor;
    PlayerControls debug;
    void Awake()
    {
        debug = new PlayerControls();


        //debug.General.debug.performed
    }
    void Start()
    {
        //gunAnchor = GetComponentInChildren<Transform>();
    }

    
    void Update()
    {

        
    }

    void Aim()
    {
        float playerDistanceFromGun =Mathf.Sqrt(Mathf.Pow(player.position.z - gunAnchor.position.z,2) + Mathf.Pow(player.position.x - gunAnchor.position.x,2));
        float bodyRotation = Mathf.Atan2(player.position.x - turretBody.position.x, player.position.z - turretBody.position.z) * 180/Mathf.PI;
        float gunRotation = -Mathf.Atan2(player.position.y - gunAnchor.position.y, playerDistanceFromGun) * 180/Mathf.PI;

        turretBody.rotation = Quaternion.Euler(0f,bodyRotation,0f);

        Vector3 grTemp = new Vector3(gunRotation,0f,0f);
        gunAnchor.localEulerAngles = grTemp;
    }

    void fire()
    {
        
    }

}
