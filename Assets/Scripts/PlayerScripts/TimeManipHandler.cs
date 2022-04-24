using UnityEngine;

public enum TimeState
{
    normal,Reverse,Slowed,Stopped,TransitionPause,revertToCheckpoint
}
public class TimeManipHandler : MonoBehaviour
{
    public SoundManager sound;

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

    private TimeState target;

    private RewindList rewindAbility;

    private PlayerControls p;

    private float transitionRemaining;

    void Awake()
    {
        p = new PlayerControls();
        p.timeStuff.normal.performed += c =>UserToNormalTime();
        p.timeStuff.slow.performed += c =>toTransition(TimeState.Slowed,0.05f);
        p.timeStuff.stop.performed += c =>toTransition(TimeState.Stopped,0.05f);;
        p.timeStuff.reverse.performed += c =>toTransition(TimeState.Reverse,0.05f);;
    }
    void OnEnable()
    {
        p.timeStuff.Enable();
    }
    void OnDisable()
    {
        p.timeStuff.Disable();
    }
    void Start()
    {
        slowPower = SlowDurration;
        stopPower = StopDurration;

        rewindAbility = new RewindList();
    }
    void Update()
    {
        switch(ts)
        {
            case TimeState.normal: normalUpdate(); break;
            case TimeState.Slowed: slowedTimeUpdate(); break;
            case TimeState.Stopped: stoppedTimeUpdate(); break;
            case TimeState.TransitionPause: TransitionPauseUpdate(); break;
            default: break;
        }

        capPower();
    }

    void FixedUpdate()
    {
        switch(ts)
        {
            case TimeState.Reverse: ReverseTimeUpdate(); break;
            case TimeState.revertToCheckpoint: revertToCheckpintUpdate(); break;
            case TimeState.TransitionPause: break;
            default: addRevertFrame(); break;
        }
    }


    private void toNormalTime()
    {
        SpecialTime.timeScale = 1.0f;
        ts = TimeState.normal;
    }
    private void UserToNormalTime()
    {
        if(ts != TimeState.revertToCheckpoint)
        {
            toTransition(TimeState.normal,0.05f);
        }
    }
    private void toSlowedTime()
    {
        SpecialTime.timeScale = slow;
        ts = TimeState.Slowed;
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
    private void toTransition(TimeState t, float pauseLength = 0.33f)
    {
        if(ts != TimeState.revertToCheckpoint || 
        (ts == TimeState.revertToCheckpoint && t == TimeState.normal))
        {
            if(ts != TimeState.TransitionPause)
            {
                ts = TimeState.TransitionPause;
                target = t;
                transitionRemaining = pauseLength;
                SpecialTime.timeScale = stopped;
            }
        }
    }

    private void toCheckpoint()
    {
        SpecialTime.timeScale = -1.5f;
        ts = TimeState.revertToCheckpoint;
    }
    private void normalUpdate()
    {
        slowPower += powerRegen * Time.deltaTime;

        stopPower += powerRegen * Time.deltaTime;
    }
    private void slowedTimeUpdate()
    {
        slowPower -= Time.deltaTime;

        stopPower += powerRegen * 0.1f * Time.deltaTime;

        if( slowPower < 0)
        {
            slowPower = 0;
            toTransition(TimeState.normal,0.1f);
        }
    }
    private void stoppedTimeUpdate()
    {
        slowPower += 0.5f * Time.deltaTime;

        stopPower -= Time.deltaTime;

        if(stopPower < 0)
        {
            stopPower = 0;
            toTransition(TimeState.normal,0.1f);
        }
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
        else toTransition(TimeState.normal,0.33f);
    }
    private void TransitionPauseUpdate()
    {
        if(transitionRemaining < 0)
        {
            switch(target)
            {
            case TimeState.normal: toNormalTime(); sound.Play("reverseStop"); break;
            case TimeState.Slowed: toSlowedTime(); break;
            case TimeState.Stopped: toStoppedTime(); sound.Play("Stop");break;
            case TimeState.Reverse: toReverseTime(); sound.Play("reverseStart");break;
            case TimeState.revertToCheckpoint: toCheckpoint(); break;
            default: toNormalTime(); break;
            }
        }
        else
        {
            transitionRemaining -= Time.deltaTime;
        }
    }
    private void revertToCheckpintUpdate()
    {
        if(SpecialTime.GAME_TIME > 0)
        {
            RevertFrame r = rewindAbility.popTil(SpecialTime.GAME_TIME);
            if(r != null)
            {
                transform.position = r.pos;
                transform.rotation = r.rot;
            }
        }
        else toTransition(TimeState.normal);
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
        
    }

    public void revertToCheckpoint()
    {
        toTransition(TimeState.revertToCheckpoint,0.5f);
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
