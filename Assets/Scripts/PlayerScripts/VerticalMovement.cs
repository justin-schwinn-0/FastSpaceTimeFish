using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public CharacterController c;
    public float jumpVelocity = 4;

    private PlayerControls p;

    public float gravity = 10;

    private float yVel;
    private float yPos;
    private float lastYPos;
    // Update is called once per frame

    void Awake()
    {
        p = new PlayerControls();

        p.movement.jump.performed += c => jump();
    }
    void OnEnable()
    {
        p.movement.Enable();
    }
    void OnDisable()
    {
        p.movement.Disable();
    }

    void Update()
    {
        if(c.isGrounded)
        {
            yVel = 0;
        }
        else
        {
            yVel = yVel - gravity * Time.deltaTime;
        }

        c.Move(new Vector3(0,yVel*Time.deltaTime,0));
    }
    
    void jump()
    {
        if(!c.isGrounded)
        {
            yVel = jumpVelocity;
            Debug.Log("also jumped");
        }
        else
            Debug.Log("jump called");
    }

    float verticalComponemnt()
    {
        return yVel;
    }
}
