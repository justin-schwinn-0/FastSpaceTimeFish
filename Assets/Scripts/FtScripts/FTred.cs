using UnityEngine;

public class FTRed : MonoBehaviour
{
    private static int lolindex;
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
                {
                    t.revertToCheckpoint();
                    switch(lolindex++)
                    {
                        case 0: SoundManager.instance.Play("BoomUrBad"); break;
                        case 1: SoundManager.instance.Play("stupid"); break;
                        case 2: SoundManager.instance.Play("laugh"); break;
                        case 3: SoundManager.instance.Play("bruh"); break;
                        case 4: SoundManager.instance.Play("bonk"); break;
                        case 5: SoundManager.instance.Play("disgustin"); break;
                        default: SoundManager.instance.Play("bruh"); break;
                    }

                    if(lolindex > 5)
                        lolindex = 0;
                    
                }
            }
        }
        else if(c.gameObject.CompareTag("FTblue"))
            c.gameObject.SetActive(false);
    }
    
}
