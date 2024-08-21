using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chaingun : MonoBehaviour
{
    [Header("Firerate")]
    public float J_FireRate;
    public float J_OgFireRate = 0.5f;

    [Header("Shooting stuff")]
    public Transform J_FireTransform; // where to fire bullets
    public float J_BasicLaunchForce = 50f;
    public int J_Ammo = 0;
    public Rigidbody J_Bullet;
    public TextMeshProUGUI J_AmmoCounterText;
    private void Update()
    {
        CheckFire();
        int Ammo = J_Ammo; J_AmmoCounterText.text = string.Format("Ammo: {00}", Ammo);
    }
    void CheckFire()
    {
        if (J_Ammo > 0f)
        {
            if (J_FireRate >= 0f)
                J_FireRate -= Time.deltaTime;   
            if (Input.GetMouseButton(0) && J_FireRate <= 0f)
            {
                Fire();
                J_FireRate = J_OgFireRate;
            }
        }     
    }
    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(J_Bullet, J_FireTransform.position, J_FireTransform.rotation);
        shellInstance.velocity = J_BasicLaunchForce * J_FireTransform.forward;
        J_Ammo = J_Ammo - 1;
    }
    public void AddAmmo(int newAmmo)
    {
        J_Ammo += newAmmo;
    }
}
