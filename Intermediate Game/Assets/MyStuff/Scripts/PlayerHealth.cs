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
    public GameManager gameManager;
   
    private void OnEnable()
    {
        J_CurrentHealth = J_StartingHealth;
        J_Dead = false;
    }
    public void TakeDamage(float amount)
    {
        if (CompareTag("Player"))
        {
            gameManager.ApplyLowHealth();
        }
        J_CurrentHealth -= amount;
        if (J_CurrentHealth <= 0f)
        {
            J_Dead = true;
            gameObject.SetActive(false);
        }
    }
 
    public void AddHealth(int newHealth)
    {
        gameManager.RevertLowHealth();
        if (J_CurrentHealth > J_StartingHealth)
        {
            J_CurrentHealth = J_StartingHealth;
        }
    }
    
}
