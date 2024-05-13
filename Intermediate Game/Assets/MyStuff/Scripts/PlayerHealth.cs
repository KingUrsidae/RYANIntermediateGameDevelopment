using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float J_StartingHealth = 1000f;
    /*
    public float J_HPFreashHold = 75f;
    public float J_HPFreashHold2 = 50f;
    public float J_HPFreashHold3 = 25f;
    public GameObject J_HealthState;
    public GameObject J_HealthState2;
    public GameObject J_HealthState3;
    */
    public float J_CurrentHealth;
    private bool J_Dead;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        J_CurrentHealth = J_StartingHealth;
        J_Dead = false;
        /*
        J_HealthState.gameObject.SetActive(false);
        J_HealthState2.gameObject.SetActive(false);
        J_HealthState3.gameObject.SetActive(false);
        */
    }

    public void TakeDamage(float amount)
    {
        J_CurrentHealth -= amount;
        if (J_CurrentHealth <= 0f && !J_Dead)
        {
            OnDeath();
        }
        /*
        if (J_CurrentHealth <= J_HPFreashHold)
        {
             J_HealthState.gameObject.SetActive(true);
        }
        if (J_CurrentHealth <= J_HPFreashHold2)
        {
             J_HealthState2.gameObject.SetActive(true);
        }
        if (m_CurrentHealth <= J_HPFreashHold3)
        {
             J_HealthState3.gameObject.SetActive(true);
        }
        */
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
        /*
        if (J_CurrentHealth >= J_HPFreashHold)
        {
             J_HealthState.gameObject.SetActive(false);
        }

        if (m_CurrentHealth2 >= J_HPFreashHold2)
        {
             J_HealthState.gameObject.SetActive(false);
        }

        if (m_CurrentHealth >= J_HPFreashHold3)
        {
             J_HealthState3.gameObject.SetActive(false);
        }
        */
    }
}
