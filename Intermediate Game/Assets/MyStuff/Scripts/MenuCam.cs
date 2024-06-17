using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    void Update()
    {
       gameObject.transform.Rotate(0, Time.deltaTime * 30, 0); 
    }
}
