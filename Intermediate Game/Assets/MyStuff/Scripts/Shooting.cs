using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// This is the shooting script, this allows for firing and ui elements.
/// </summary>
public class Shooting : MonoBehaviour
{
    [Header("Firerate")]
    public float J_FireRate = 0.5f;
    private float timeToFire = 0.01f;

    [Header("Shooting stuff")]
    public Transform m_FireTransform;
    public float J_BasicLaunchForce = 200f;
    public int J_Ammo = 0;
    public Rigidbody J_Bullet; public Rigidbody J_Bullet2;
    public TextMeshProUGUI J_AmmoCounterText;  

    private void Awake()
    {
        
    }
    private void Update()
    {
        CheckFire();
        int Ammo = J_Ammo; J_AmmoCounterText.text = string.Format("Ammo: {00}", Ammo);
    }
    void CheckFire()
    {
        if (timeToFire > 0f)
        {
            timeToFire -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0) && timeToFire <= 0)
        {
            Fire();
            timeToFire = J_FireRate;
        }
        if (Input.GetMouseButton(1) && timeToFire <= 0 && J_Ammo > 0)
        {
            Fire2();
            timeToFire = J_FireRate + 0.5f;
        }
    }
    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(J_Bullet, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = J_BasicLaunchForce * m_FireTransform.forward;
    }
       private void Fire2()
    {
        J_Ammo = J_Ammo - 1;
        Rigidbody shellInstance = Instantiate(J_Bullet2, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = J_BasicLaunchForce * m_FireTransform.forward;

    }
    public void AddAmmo(int newAmmo)
    {
        J_Ammo += newAmmo;
    }
}
