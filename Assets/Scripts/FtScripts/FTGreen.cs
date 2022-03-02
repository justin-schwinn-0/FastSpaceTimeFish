using UnityEngine;

public class FTgreen : MonoBehaviour
{
    void Awake()
    {
        gameObject.tag = "FTgreen";

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    // called when the player enters the trigger
    void OnTriggerEnter(Collider c)
    {

    }

    void update()
    {

    }

    bool isActive(bool test)
    {
        return test;
    }
    bool isActive()
    {
        return false;
    }
}