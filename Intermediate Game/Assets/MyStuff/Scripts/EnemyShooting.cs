using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Rigidbody J_Shell;
    public Transform J_FireTransform;
    public float J_lanchForce;
    public float J_ShootDeley;

    private bool J_CanShoot;
    private float J_ShootTimer;
    private void Awake()
    {
        J_CanShoot = false;
        J_ShootTimer = 0;
    }
    private void Update()
    {
        if (J_CanShoot == true)
        {
            J_ShootTimer -= Time.deltaTime;
            if (J_ShootTimer <= 0)
            {
                J_ShootTimer = J_ShootDeley;
                Fire();
            }
        }
    }
    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(J_Shell, J_FireTransform.position, J_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = J_lanchForce * J_FireTransform.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            J_CanShoot = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            J_CanShoot = false;
        }
    }
}
