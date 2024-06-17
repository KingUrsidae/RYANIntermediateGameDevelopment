using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoKits : MonoBehaviour
{
    public Shooting shooting;
    public int J_AddedAmmo = 10;

    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime * 60, 0);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shooting.AddAmmo(J_AddedAmmo);
            Destroy(gameObject);
        }
    }
}
