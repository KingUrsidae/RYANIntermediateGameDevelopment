using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float J_StartingHealth = 2f;
    
    public GameObject J_HealthStateLow;
        
    public float J_CurrentHealth;
    private bool J_Dead;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        J_CurrentHealth = J_StartingHealth;
        J_Dead = false;
        
        J_HealthStateLow.gameObject.SetActive(false);
        
    }

    public void TakeDamage(float amount)
    {
        J_CurrentHealth -= amount;
        if (J_CurrentHealth <= 0f && !J_Dead)
        {
            OnDeath();
        }
        
        if (J_CurrentHealth <= 1f)
        {
             J_HealthStateLow.gameObject.SetActive(true);
        }
                
    }
    private void OnDeath()
    {
        J_Dead = true;

        gameObject.SetActive(false);
    }

    public void AddHealth(int newHealth)
    {
        J_CurrentHealth += newHealth;
        if (J_CurrentHealth > J_StartingHealth)
        {
            J_CurrentHealth = J_StartingHealth;
        }
        
        if (J_CurrentHealth >= 1f)
        {
             J_HealthStateLow.gameObject.SetActive(false);
        }
                
    }
}
