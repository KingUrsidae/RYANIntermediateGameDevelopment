using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShooting : MonoBehaviour
{
    [Header("Bullet Factors")]
    public Rigidbody J_Shell;
    public Transform J_FireTransform;
    public float J_lanchForce;
    public float J_ShootDeley;
    private GameObject J_Player;
    public GameObject J_Gun;
    
    private bool J_CanShoot;
    private float J_ShootTimer;
    private void Awake()
    {
        J_CanShoot = false;
        J_ShootTimer = 0;
        J_Player = GameObject.FindGameObjectWithTag("PlayerTarget");
    }
    private void Update()
    {
        if (J_CanShoot == true)
        {
            Aim();
            J_ShootTimer -= Time.deltaTime;
            if (J_ShootTimer <= 0)
            {
                J_ShootTimer = J_ShootDeley;
                Fire();
            }
        }
    }
    private void Aim()
    {
       J_Gun.transform.LookAt(J_Player.transform.position);
    }
    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(J_Shell, J_FireTransform.position, J_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = J_lanchForce * J_FireTransform.forward;
    }
    private void OnTriggerStay(Collider other)
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
