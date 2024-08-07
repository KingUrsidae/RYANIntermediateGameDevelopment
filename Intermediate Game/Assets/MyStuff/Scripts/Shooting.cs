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
    private float timeToFire;

    [Header("Shooting stuff")]
    public Transform m_FireTransform;
    public float J_BasicLaunchForce = 50f;
    public int J_Ammo = 0;
    public Rigidbody J_Bullet; public Rigidbody J_Bullet2;
    public TextMeshProUGUI J_AmmoCounterText;
    public bool InfiniteAmmo = false;
    public GameObject J_FireBall;
    //cheats
    private bool NoFireRate = false;
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
        if(timeToFire > 0.1f)
        {
            J_FireBall.SetActive(false);
        }
        J_FireBall.SetActive(false);
        if (Input.GetMouseButton(0) && timeToFire <= 0)
        {
            Fire();
            if (NoFireRate == false)
            {
                timeToFire = J_FireRate;
            }
            else
            {
                timeToFire = 0.04f;
            }
        }
        if (Input.GetMouseButton(1) && timeToFire <= 0 && J_Ammo > 0)
        {
            Fire2();
            if (NoFireRate == false)
            {
                timeToFire = J_FireRate + 0.5f;
            }
            else
            {
                timeToFire = 0.06f;
            }
        }
    }
    private void Fire()
    {
        J_FireBall.SetActive(true);
        Rigidbody shellInstance = Instantiate(J_Bullet, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = J_BasicLaunchForce * m_FireTransform.forward;
    }
       private void Fire2()
    {
        J_FireBall.SetActive(true);
        Rigidbody shellInstance = Instantiate(J_Bullet2, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = J_BasicLaunchForce * m_FireTransform.forward;
        J_Ammo = J_Ammo - 1;
    }
    public void AddAmmo(int newAmmo)
    {
        J_Ammo += newAmmo;
    }
    public void ChangeFireRate()
    {
        NoFireRate = true;
    }
}
