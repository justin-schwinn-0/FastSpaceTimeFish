using UnityEngine;


public class TimeManipHandler : MonoBehaviour
{
    public float reverse = -1.0f;
    public float normal = 1.0f;
    public float Slow = 0.01f;
    private float stopped = 0f;

    private PlayerControls p;



    public float slowPowerCap = 10.0f;
    public float stopPowerCap = 5.0f;
    public float reverseAllowance = 3.0f;

    public float SlowPowerConsume = 5.0f;

    public float stopPowerConsume = 1.0f;
    public float powerRegen = -1.0f;




    private float slowPower;
    private float stopPower;
    private float reverseLimit;


    private float slowConsumption;
    private float stopConsumption;

    private RewindList rewindAbility;


    private bool reversing;

    void Awake()
    {
        p = new PlayerControls();
        p.timeStuff.normal.performed += c =>normalTime();
        p.timeStuff.slow.performed += c =>slowTime();
        p.timeStuff.stop.performed += c =>stopTime();
        p.timeStuff.reverse.performed += c =>reverseTime();
    }
    void OnEnable()
    {
        p.timeStuff.Enable();
    }
    void OnDisable()
    {
        p.timeStuff.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        slowPower = slowPowerCap;
        stopPower = stopPowerCap;

        rewindAbility = new RewindList();
    }

    // Update is called once per frame
    void Update()
    {
        if(!reversing)
        {
            Vector3 v = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            RevertFrame r = new RevertFrame(SpecialTime.GAME_TIME, transform.rotation, v);
            
            rewindAbility.addFrame(r);

            reverseLimit = SpecialTime.GAME_TIME - reverseAllowance;

            if(reverseLimit < 0)
                reverseLimit = 0;

            rewindAbility.cullFrames(reverseLimit);
        }
        else
        {
            if(SpecialTime.GAME_TIME > reverseLimit)
            {
                RevertFrame r = rewindAbility.popTil(SpecialTime.GAME_TIME);
                if(r != null)
                {
                    transform.position = r.pos;
                    transform.rotation = r.rot;
                }
            }
            else normalTime();
        }
        
        checkPower();
    }

    private void reverseTime()
    {
        SpecialTime.timeScale = reverse;
        slowConsumption = 0;
        stopConsumption = 0;
        reversing = true;
    }
    private void normalTime()
    {
        SpecialTime.timeScale = normal;
        slowConsumption = powerRegen;
        stopConsumption = powerRegen;
        reversing = false;
    }

    private void slowTime()
    {
        SpecialTime.timeScale = Slow;
        slowConsumption = SlowPowerConsume;
        stopConsumption = powerRegen * 1/5;
        reversing = false;
    }
    private void stopTime()
    {
        SpecialTime.timeScale = stopped;
        slowConsumption = powerRegen * 0.1f;
        stopConsumption = stopPowerConsume;
        reversing = false;
    }
    private void checkPower()
    {
        slowPower -= slowConsumption * Time.deltaTime;
        stopPower -= stopConsumption * Time.deltaTime;

        if(slowPower > slowPowerCap)
        {
            slowPower = slowPowerCap;
        }
        else if( slowPower < 0)
        {
            slowPower = 0;
            normalTime();
        }

        if(stopPower > stopPowerCap)
        {
            stopPower = stopPowerCap;
        }
        else if(stopPower < 0)
        {
            stopPower = 0;
            normalTime();
        }
    }

    public float getslowPower()
    {
        return slowPower;
    }
    public float getStopPower()
    {
        return stopPower;
    }


}
