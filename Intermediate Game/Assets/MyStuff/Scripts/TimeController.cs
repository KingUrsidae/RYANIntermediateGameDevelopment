using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowDownLength = 2f;
    
    /*
    timeScale
    1 =  realtime
    .5 = 2x slower
    adjust bettween 0 and 1

    */
    public void DoSlowdown()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    
    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
}
// reffrence to youtube video by "Brackys" avalible at: https://youtu.be/0VGosgaoTsw?si=XBetxNsz6R1WbAqr