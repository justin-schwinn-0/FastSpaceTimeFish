using UnityEngine;

public class FTRed : MonoBehaviour
{
    void Awake()
    {
        gameObject.tag = "FTred";

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            TimeManipHandler t = c.GetComponent<TimeManipHandler>();

            if(t != null)
            {
                if(t.getTimeState() != TimeState.revertToCheckpoint)
                    t.revertToCheckpoint();
            }
        }
        else if(c.gameObject.CompareTag("FTblue"))
            Destroy(c.gameObject);
    }
    
}
