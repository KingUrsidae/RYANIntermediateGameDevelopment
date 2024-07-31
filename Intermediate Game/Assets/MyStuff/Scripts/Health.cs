using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the health code for both enemies and the player, it reffrences the game manager for cidimatic effects but only for the player. 
/// </summary>
public class Health : MonoBehaviour
{
    [Header("Health")]
    public float J_StartingHealth = 2f;
    public float J_CurrentHealth;
    public GameManager gameManager;
    private void OnEnable()
    {
        J_CurrentHealth = J_StartingHealth;
    }
    public void TakeDamage(float damage)
    {
        J_CurrentHealth -= damage;
        if (CompareTag("Player"))
        {
            gameManager.ApplyLowHealth();
        }
    }
    private void Update()
    {
        if (J_CurrentHealth <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    public void AddHealth(int newHealth)
    {
        if (CompareTag("Player"))
        {
            gameManager.RevertLowHealth();
        }
        if (J_CurrentHealth > J_StartingHealth)
        {
            J_CurrentHealth = J_StartingHealth;
        }
    }
    public void GlassModeOn()
    {
        J_CurrentHealth = 1f;
        J_StartingHealth = 1f;
    }
}
