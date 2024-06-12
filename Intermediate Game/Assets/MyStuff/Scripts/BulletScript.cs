using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Bullet Numbers")]
    public float J_Damage = 1f;
    private float J_MaxLifeTime = 20f;
    private float J_ExplosionRadius = 15;
    private float J_ExplosionForce = 300f;

    public ParticleSystem m_ExplosionParticles;
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
            PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth>();
            if (targetHealth != null)
            {
                float damage = J_Damage; 
                targetHealth.TakeDamage(damage);
            }
        }
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }
}
