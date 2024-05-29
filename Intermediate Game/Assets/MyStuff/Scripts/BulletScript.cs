using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float J_MaxLifeTime = 20f;
    public float J_MaxDamage = 120;
    public float J_ExplosionRadius = 15;
    public float J_ExplosionForce = 300f;

    public ParticleSystem m_ExplosionParticles;
    private void Start()
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
                float damage = CalculateDamage(targetRigidbody.position);
                targetHealth.TakeDamage(damage);
            }
        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (J_ExplosionRadius - explosionDistance) / J_ExplosionRadius;

        float damage = relativeDistance * J_MaxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
