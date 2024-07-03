using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float J_Damage = 20f;
    public float J_ExplosionRadius = 20;
    public float J_ExplosionForce = 100f;
    public ParticleSystem J_ExplosionParticles;

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
