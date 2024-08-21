using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Bullet Numbers")]
    public float J_Damage = 1f;
    public float J_MaxLifeTime = 5f;
    public float J_ExplosionRadius = 0.5f;
    public float J_ExplosionForce = 1f;
    //public ParticleSystem J_ExplosionParticles;
    private void FixedUpdate()
    {
        Destroy(gameObject, J_MaxLifeTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        Collider[] enemeies = Physics.OverlapSphere(transform.position, J_ExplosionRadius);
        foreach (Collider enemey in enemeies)
        {
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
        }
        /*
        J_ExplosionParticles.transform.parent = null;
        J_ExplosionParticles.Play();
        Destroy(J_ExplosionParticles.gameObject, J_ExplosionParticles.main.duration);
        */
        //Destroy(gameObject);
    }
    private float CalculateDamage()
    {
        float damage = J_Damage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
    /// <summary>
    /// Draws AOE (Area of effect) for damage
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, J_ExplosionRadius);
    }
}
