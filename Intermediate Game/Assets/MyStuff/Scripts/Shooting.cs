using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public Transform m_FireTransform;
    public float J_BasicLaunchForce = 50f;
    public int J_Ammo = 0;
    public Rigidbody J_Bullet; public Rigidbody J_Bullet2;
    public TextMeshProUGUI J_AmmoCounterText;
    
    private void Update()
    {
        int Ammo = J_Ammo; J_AmmoCounterText.text = string.Format("Ammo: {00}", Ammo);
        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
        if (Input.GetButtonUp("Fire2") && J_Ammo > 0)
        {
            Fire2();
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
