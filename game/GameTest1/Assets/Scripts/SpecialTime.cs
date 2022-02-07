using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTime : MonoBehaviour
{
    public static float GAME_TIME;
    public static float timeScale = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        GAME_TIME = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GAME_TIME += DeltaTime();
    }
    public static float DeltaTime()
    {
        return Time.deltaTime * timeScale;
    }
}
