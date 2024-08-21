using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float J_StartingHealth = 2f;
    public float J_CurrentHealth;
    //public GameManager gameManager;
    private void OnEnable()
    {
        J_CurrentHealth = J_StartingHealth;
    }
    public void TakeDamage(float damage)
    {
        J_CurrentHealth -= damage;
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
        J_CurrentHealth = newHealth;
        if (J_CurrentHealth > J_StartingHealth)
        {
            J_CurrentHealth = J_StartingHealth;
        }
    }
}
