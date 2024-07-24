using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [Header("Explosion Numbers")]
    private float J_ExplosionRadius = 7;
    public float J_ExplosionForce = 100f;
    public ParticleSystem J_ExplosionParticles;
    public SphereCollider J_sphere;
    public float J_Damage = 10f;
    private void Start()
    {
        GetComponent<SphereCollider>();
        J_sphere.enabled = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        J_sphere.enabled = true;
        J_ExplosionParticles.transform.parent = null;
        J_ExplosionParticles.Play();
        Destroy(gameObject);
    }    
    public void OnTriggerEnter(Collider other)
    {
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (targetRigidbody != null)
        {
            targetRigidbody.AddExplosionForce(J_ExplosionForce, transform.position, J_ExplosionRadius);
            Health targetHealth = targetRigidbody.GetComponent<Health>();
            if (targetHealth != null && J_sphere.enabled == true)
            {
                float damage = CalculateDamage();
                targetHealth.TakeDamage(damage);
            }
        }
    }
    private float CalculateDamage()
    {
        J_sphere.enabled = false;
        float damage = J_Damage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
