using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public Transform cam;
    public Text debugText;
    public float MaxMovementSpeed = 5.0f;



    PlayerControls p;

    private Vector2 si;

    private float turnSmoothTime = 0.05f;
    private float turnVelocity;

   
    void Awake()
    {
        p = new PlayerControls();

        p.movement.move.performed += c =>  si = c.ReadValue<Vector2>();
        p.movement.move.canceled += c =>  dampInput();
    }
    void OnEnable()
    {
        p.movement.Enable();
    }
    void OnDisable()
    {
         p.movement.Disable();
    }
    void Start()
    {
    }

    void Update()
    {
        newMove();
    }

    void newMove()
    {
        float h = si.x;
        float v = si.y;
        Vector3 direction = new Vector3(h,0,v);

        if(direction.magnitude >= 0.1f)
        {
            float targetA = Mathf.Atan2(h,v) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetA,ref turnVelocity, turnSmoothTime);
            transform.rotation= Quaternion.Euler(0,angle,0);

            Vector3 movD = Quaternion.Euler(0f,targetA,0f) * Vector3.forward;

            controller.Move(movD.normalized * MaxMovementSpeed * Time.deltaTime);
        }

    }
    void dampInput()
    {
        si = si * 0.099f;
    }
}
