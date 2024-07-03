using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
/// <summary>
/// This script is the controller of time! Its what allows bullet time to work. It ajusts the time scale for a limited amount of time based of of the slow down length 
/// and also adjusts the total speed of time with the slow down factor. 
/// </summary>
public class TimeController : MonoBehaviour
{
    [Header("Time factors")]
    public float slowdownFactor = 0.5f; //0.05f;
    public float slowDownLength = 2f;
    [Header("Camera effects")]
    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private float originalVignetteIntensity = 0f;
    private float originalChromeaticAberratonIntensity = 0f;
    public float J_timeScale;
    
       
    /// <summary>
    /// timeScale
    /// 1 =  realtime
    /// .5 = 2x slower
    /// adjust between 0 and 1
    /// </summary>
    public void DoSlowdown()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .1f;
        ApplyCinematicEffects();
    }
    void Update()
    {
        // Bullet time code
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        // Cidimatic effects
        if (Time.timeScale <= 1f)
        {
            J_timeScale -= 0.0001f;
        }
        if (Time.timeScale >= 1f)
        {
            RevertCinematicEffects();
        }
        if (vignette != null)
        {
            vignette.intensity.value = J_timeScale;
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = J_timeScale;
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
        J_timeScale = 0f;
    }
    private void ApplyCinematicEffects()
    {
        J_timeScale = 0.65f;
    }
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