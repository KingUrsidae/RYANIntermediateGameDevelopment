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
    /// <summary>
    /// Bullet time code in update
    /// </summary>
    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    /// <summary>
    /// Cidimatic effects in fixed update
    /// </summary>
    private void FixedUpdate()
    {
        if (Time.timeScale <= 1f)
        {
            J_timeScale -= 0.001f;
        }
        if (Time.timeScale <= 0f)
        {
            J_timeScale = 0f;
            RevertCinematicEffects();
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = J_timeScale;
        }
        if (vignette != null)
        {
            vignette.intensity.value = J_timeScale;
        }
    }
    private void Start()
    {
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            originalVignetteIntensity = vignette.intensity.value;
        }
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        J_timeScale = 0f;
    }
    private void ApplyCinematicEffects()
    {
        J_timeScale = 0.6f;
    }
    private void RevertCinematicEffects()
    {
        if (vignette != null)
        {
            vignette.intensity.value = originalVignetteIntensity;
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = 0.01f;
        }
    }

}
// reffrence to youtube video by "Brackeys" avalible at: https://youtu.be/0VGosgaoTsw?si=XBetxNsz6R1WbAqr