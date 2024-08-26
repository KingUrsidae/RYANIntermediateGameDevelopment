using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Chaingun : MonoBehaviour
{
    [Header("Firerate")]
    public float FireRate; // live fire rate
    public float OgFireRate = 0.25f; // used to reset time

    [Header("Shooting stuff")]
    public Transform J_FireTransform; // where to fire bullets
    public float BasicLaunchForce = 50f;
    public int J_Ammo = 0;
    public Rigidbody J_Bullet; // what bullet to shoot
    public TextMeshProUGUI AmmoCounterText; // ui

    [Header("Rotate barrel")]
    public GameObject Barrels;
    public float rotateSpeed = 360; // has to equll int when devided by 6 (amount of barrels)
    float originalRotateSpeed; // save rotate speed
    public float slowdownAmount = 60; // slow down rate after shooting

    private void Start()
    {
        originalRotateSpeed = rotateSpeed;
        rotateSpeed = 0;
    }
    private void Update()
    {
        CheckFire();
        int Ammo = J_Ammo; AmmoCounterText.text = string.Format("Ammo: {00}", Ammo);

        Barrels.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
       if(rotateSpeed > 0)
        {
            rotateSpeed -= Time.deltaTime * slowdownAmount;
        }
       else
        {
            rotateSpeed = 0;
        }
      
    }
    void CheckFire()
    {
        if (J_Ammo > 0f)
        {
            if (FireRate >= 0f)
                FireRate -= Time.deltaTime;   
            if (Input.GetMouseButton(0) && FireRate <= 0f)
            {
                Fire();
                FireRate = OgFireRate;
            }
        }     
    }
    private void Fire()
    {
        rotateSpeed = originalRotateSpeed;
        Rigidbody shellInstance = Instantiate(J_Bullet, J_FireTransform.position, J_FireTransform.rotation);
        shellInstance.velocity = BasicLaunchForce * J_FireTransform.forward;
        J_Ammo = J_Ammo - 1;
    }
    public void AddAmmo(int newAmmo)
    {
        J_Ammo += newAmmo;
    }
}
