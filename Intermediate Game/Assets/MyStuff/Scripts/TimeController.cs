using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TimeController : MonoBehaviour
{
    [Header("Time factors")]
    public float slowdownFactor = 0.05f;
    public float slowDownLength = 2f;
    [Header("Camera effects")]
    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private float originalVignetteIntensity;
    private float originalChromeaticAberratonIntensity;
    public float J_newChromeaticAberratonIntensity;
    public float J_newVignette;
    public float J_timeScale;
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
        //ApplyCinematicEffects();
    }
    
    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        if (Time.timeScale >= 1f)
        {
            RevertCinematicEffects();
        }
        J_timeScale = Time.timeScale;
        if (vignette != null)
        {
            vignette.intensity.value = J_timeScale;
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = J_timeScale ;
        }
    }
    private void Start()
    {
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            originalVignetteIntensity = vignette.intensity.value;
        }
        if (postProcessVolume.profile.TryGetSettings(out chromaticAberration))
        {
            originalChromeaticAberratonIntensity = chromaticAberration.intensity.value;
        }

    }
    /*
    private void ApplyCinematicEffects()
    {
        if (vignette != null)
        {
            vignette.intensity.value = J_newVignette;
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = J_newChromeaticAberratonIntensity;
        }
    }
    */
    private void RevertCinematicEffects()
    {
        if (vignette != null)
        {
            vignette.intensity.value = originalVignetteIntensity;
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = originalChromeaticAberratonIntensity;
        }
    }

}
// reffrence to youtube video by "Brackeys" avalible at: https://youtu.be/0VGosgaoTsw?si=XBetxNsz6R1WbAqr