using UnityEngine;
public class FTGreen : MonoBehaviour
{
    public bool persistant;
    bool pressed;
    bool hasBeenActivated;

    void Awake()
    {
        gameObject.tag = "FTgreen";

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    // called when the player enters the trigger
    void OnTriggerEnter(Collider c)
    {

        if(c.CompareTag("PLayer"))
        {
            if(persistant)
            {
                hasBeenActivated = true;
            }
            else 
            {
                pressed = true;
            }
        }
    }
    void OnTriggerExit(Collider c)
    {
        pressed = false;
    }

    public bool isActive(bool test)
    {
        return test;
    }
    public bool isActive()
    {
        return (persistant) ? hasBeenActivated : pressed;
    }
}