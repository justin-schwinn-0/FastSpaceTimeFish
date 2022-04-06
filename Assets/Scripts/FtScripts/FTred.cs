using UnityEngine;

public class FTRed : MonoBehaviour
{
    void Awake()
    {
        gameObject.tag = "FTred";

        if(gameObject.GetComponent<BoxCollider>() != null)
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        else 
        {
            gameObject.GetComponent<MeshCollider>().convex = true;
            gameObject.GetComponent<MeshCollider>().isTrigger = true;
        }
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
            c.gameObject.SetActive(false);
    }
    
}
