using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    /// <summary>
    /// This makes the menu cam rotate!!!!!
    /// </summary>
    void Update()
    {
       gameObject.transform.Rotate(0, Time.deltaTime * 20, 0); 
    }
}
