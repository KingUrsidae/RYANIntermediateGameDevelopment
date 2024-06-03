using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float J_StartingHealth = 2f;
    public float J_CurrentHealth;
    private bool J_Dead;

    [Header("Effects")]
    public PostProcessVolume postProcessVolume;
    public float J_newTempreture;
    private float originalTempreture;
    private float originalExposure;
    private ColorGrading colorGrading;
    private AutoExposure autoExposure;
    private void Awake()
    {
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            originalTempreture = colorGrading.temperature.value;
        }
        if (postProcessVolume.profile.TryGetSettings(out autoExposure))
        {
            //originalExposure = autoExposure..value;
        }
    }
    private void OnEnable()
    {
        J_CurrentHealth = J_StartingHealth;
        J_Dead = false;
    }
    public void TakeDamage(float amount)
    {
        J_CurrentHealth -= amount;ApplyLowHealth();
        if (J_CurrentHealth <= 0f && !J_Dead)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        J_Dead = true;
        if (autoExposure != null)
        {
            //autoExposure..value = 0f;
        }
        gameObject.SetActive(false);
    }
    public void AddHealth(int newHealth)
    {
        J_CurrentHealth += newHealth;RevertLowHealth();
        if (J_CurrentHealth > J_StartingHealth)
        {
            J_CurrentHealth = J_StartingHealth;
        }
    }
    private void ApplyLowHealth()
    {
        if (colorGrading != null)
        {
            colorGrading.temperature.value = J_newTempreture;
        }
    }
    private void RevertLowHealth()
    {
        if (colorGrading != null)
        {
            colorGrading.temperature.value = originalTempreture;
        }
    }
    /*
    private void Update()
    {
        if (J_CurrentHealth == 0 )
        {
            if (autoExposure != null)
            {
                autoExposure..value = 0f;
            }
        }
        
    }
    */
}
