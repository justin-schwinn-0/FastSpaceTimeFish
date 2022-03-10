using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int SceneToLoad;

    void Awake()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
            SceneManager.LoadScene(SceneToLoad);
    }
}
