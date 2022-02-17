using UnityEngine;

public enum TimeState
{
    Reverse,normal,Slow,Stopped,TransitionPause
}
public class TimeManipHandler : MonoBehaviour
{
    public float reverse = -1.0f;
    public float slow = 0.01f;
    private float stopped = 0f;

    public float SlowDurration = 10.0f;
    public float StopDurration = 5.0f;
    public float ReverseDurration = 3.0f;
    public float powerRegen = 1.0f;


    private float slowPower;
    private float stopPower;
    private float reverseLimit;

    private TimeState ts;

    private RewindList rewindAbility;

    private PlayerControls p;

    private float transitionRemaining;

    void Awake()
    {
        p = new PlayerControls();
        p.timeStuff.normal.performed += c =>toNormalTime();
        p.timeStuff.slow.performed += c =>toSlowedTime();
        p.timeStuff.stop.performed += c =>toStoppedTime();
        p.timeStuff.reverse.performed += c =>toReverseTime();
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
        slowPower = SlowDurration;
        stopPower = StopDurration;

        rewindAbility = new RewindList();
    }

    // Update is called once per frame
    void Update()
    {
        switch(ts)
        {
            case TimeState.normal: normalUpdate(); break;
            case TimeState.Slow: slowedTimeUpdate(); break;
            case TimeState.Stopped: stoppedTimeUpdate(); break;
            case TimeState.Reverse: ReverseTimeUpdate(); break;
            case TimeState.TransitionPause: TransitionPauseUpdate(); break;
        }
    }


    private void toNormalTime()
    {
        SpecialTime.timeScale = 1.0f;
        ts = TimeState.normal;
    }
    private void toSlowedTime()
    {
        SpecialTime.timeScale = slow;
        ts = TimeState.Slow;
    }
    private void toStoppedTime()
    {
        SpecialTime.timeScale = stopped;
        ts = TimeState.Stopped;
    }
    private void toReverseTime()
    {
        SpecialTime.timeScale = reverse;
        ts = TimeState.Reverse;
    }
    private void toTransition()
    {
        ts = TimeState.TransitionPause;
        transitionRemaining = 0.33f;
        SpecialTime.timeScale = stopped;
    }
    private void normalUpdate()
    {
        slowPower += powerRegen * Time.deltaTime;

        stopPower += powerRegen * Time.deltaTime;

        capPower();
        addRevertFrame();
    }
    private void slowedTimeUpdate()
    {
        slowPower -= Time.deltaTime;

        stopPower += powerRegen * 0.1f * Time.deltaTime;

        if( slowPower < 0)
        {
            slowPower = 0;
            toNormalTime();
        }

        capPower();
        addRevertFrame();
    }
    private void stoppedTimeUpdate()
    {
        slowPower += 0.5f * Time.deltaTime;

        stopPower -= Time.deltaTime;

        if(stopPower < 0)
        {
            stopPower = 0;
            toNormalTime();
        }

        capPower();
        addRevertFrame();
    }
    private void ReverseTimeUpdate()
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
        else toTransition();
    }
    private void TransitionPauseUpdate()
    {
        if(transitionRemaining < 0)
        {
            toNormalTime();
        }
        else
        {
            transitionRemaining -= Time.deltaTime;
        }
    }
    private void capPower()
    {
        if(slowPower > SlowDurration)
        {
            slowPower = SlowDurration;
        }

        if(stopPower > StopDurration)
        {
            stopPower = StopDurration;
        }
    }
    private void addRevertFrame()
    {
        Vector3 v = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        RevertFrame r = new RevertFrame(SpecialTime.GAME_TIME, transform.rotation, v);
        
        rewindAbility.addFrame(r);
        reverseLimit = SpecialTime.GAME_TIME - ReverseDurration;
        
        if(reverseLimit < 0)
            reverseLimit = 0;
        
        rewindAbility.cullFrames(reverseLimit);
    }


    public float getslowPower()
    {
        return slowPower;
    }
    public float getStopPower()
    {
        return stopPower;
    }

    public TimeState getTimeState()
    {
        return ts;
    }


}
