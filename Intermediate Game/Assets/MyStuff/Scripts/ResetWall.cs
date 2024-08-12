using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetWall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.tag == "Enemey")
        {
            Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Health targetHealth = targetRigidbody.GetComponent<Health>();
            targetHealth.TakeDamage(100f);
        }
    }
}
