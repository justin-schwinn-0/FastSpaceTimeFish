using UnityEngine;

public class FTBlue : MonoBehaviour
{
    void Awake()
    {
        gameObject.tag = "FTblue";

        Rigidbody r = gameObject.AddComponent<Rigidbody>();
        r.isKinematic = true;
    }
}
