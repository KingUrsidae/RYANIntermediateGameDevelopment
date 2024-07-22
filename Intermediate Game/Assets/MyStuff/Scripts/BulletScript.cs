using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Bullet Numbers")]
    public float J_Damage = 1f;
    private float J_MaxLifeTime = 5f;
    private float J_ExplosionRadius = 15;
    private float J_ExplosionForce = 300f;

    public ParticleSystem J_ExplosionParticles;
    private void Update()
    {
        Destroy(gameObject, J_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (targetRigidbody != null)
        {
            targetRigidbody.AddExplosionForce(J_ExplosionForce, transform.position, J_ExplosionRadius);            
            Health targetHealth = targetRigidbody.GetComponent<Health>();
            if (targetHealth != null)
            {
                float damage = CalculateDamage();
                targetHealth.TakeDamage(damage);
            }
        }
        J_ExplosionParticles.transform.parent = null;
        J_ExplosionParticles.Play();
        Destroy(J_ExplosionParticles.gameObject, J_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }
    private float CalculateDamage()
    {
        float damage = J_Damage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
