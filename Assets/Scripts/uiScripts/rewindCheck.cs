using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewindCheck : MonoBehaviour
{
    // Update is called once per frame
    public GameObject effect;
    void Update()
    {
        if(SpecialTime.timeScale < 0)
        {
            effect.SetActive(true);
        }
        else effect.SetActive(false);
    }
}
