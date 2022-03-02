using UnityEngine;

public class FTGreen : MonoBehaviour
{
    void Awake()
    {
        gameObject.tag = "FTgreen";

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    void OnTriggerEnter(Collider c)
    {
        
    }

    void update()
    {
        // do stuff every frame
    }

    bool isActive()
    {
        return false;
    }
    
}
