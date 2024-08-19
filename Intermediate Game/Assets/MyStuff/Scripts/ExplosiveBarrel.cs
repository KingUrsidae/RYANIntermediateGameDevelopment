using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [Header("Explosion Numbers")]
    public float J_ExplosionRadius = 10f;
    public float J_ExplosionForce = 100f;
    public float J_Damage = 10f; 
    public ParticleSystem J_ExplosionParticles;
    public GameObject J_ExplodingBarrel;
    public void Awake()
    {
        J_ExplodingBarrel.SetActive(true);
    }
    public void Explode()
    {
        J_ExplodingBarrel.SetActive(false);                                
        J_ExplosionParticles.transform.parent = null;
        J_ExplosionParticles.Play();
        Collider[] enemeies = Physics.OverlapSphere(transform.position, J_ExplosionRadius);
        foreach(Collider enemey in enemeies)
        {
            Rigidbody targetRigidbody = enemey.gameObject.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                targetRigidbody.AddExplosionForce(J_ExplosionForce, transform.position, J_ExplosionRadius);
                Health targetHealth = targetRigidbody.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(J_Damage);
                }
            }
        }         
    }
    /// <summary>
    /// Draws Explosive and damage range
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, J_ExplosionRadius);
    }
    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }    
}
