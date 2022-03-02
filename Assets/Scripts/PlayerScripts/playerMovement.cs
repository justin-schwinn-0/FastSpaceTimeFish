using UnityEngine;
using UnityEngine.UI;
public class playerMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public Transform cam;
    public TimeManipHandler t;

    public float jumpVelocity = 40;

    public float gravity = 100;
    public float MaxMovementSpeed = 5.0f;
    PlayerControls p;

    private Vector2 si;

    private float turnSmoothTime = 0.05f;
    private float turnVelocity;

    private float yVel;

   
    void Awake()
    {
        p = new PlayerControls();

        p.movement.move.performed += c =>  si = c.ReadValue<Vector2>();
        p.movement.move.canceled += c =>  dampInput();
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
        switch(t.getTimeState()) // switch forces this movement script to do nothing while time is being reversed
        {
            case TimeState.normal: newMove(); break;
            case TimeState.Slowed: newMove(); break;
            case TimeState.Stopped: newMove(); break;
            default: break;
        }
    }

    void newMove()
    {
        float h = si.x;
        float v = si.y;
        Vector3 direction = new Vector3(h,0,v);
        Vector3 movD = new Vector3();

        if(direction.magnitude >= 0.1f)
        {
            float targetA = Mathf.Atan2(h,v) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetA,ref turnVelocity, turnSmoothTime);
            transform.rotation= Quaternion.Euler(0,angle,0);

            movD = Quaternion.Euler(0f,targetA,0f) * new Vector3(0,0,1);

            movD = movD.normalized * MaxMovementSpeed * direction.magnitude * Time.deltaTime;
        }

        movD.y = yVel * Time.deltaTime;

        controller.Move(movD);

        if(!controller.isGrounded)
        {
            yVel -= gravity * Time.deltaTime;
        }
    }
    
    void dampInput()
    {
        si = si * 0.099f;
    }

    void jump()
    {
        if(controller.isGrounded)
        {
            yVel = jumpVelocity;
        }
    }
}
