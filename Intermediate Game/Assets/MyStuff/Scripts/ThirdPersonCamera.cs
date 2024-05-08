using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor = true; 
    // Follow the target
    public Transform target;
    public float moveSpeed = 4f;
    // rotate camera
    public float turnSpeed = 4f;
    
    public float tiltMax = 75f;
    public float tiltMin = 45f;

    private Transform pivot;
    private float tiltAngle;
    private float lookAngle;
    private Vector3 pivotEulers;

    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        pivot = GetComponentInChildren<Camera>().transform.parent;
        pivotEulers = pivot.rotation.eulerAngles;
    }    
    // Update is called once per frame
    private void Update()
    {
        HandleRotation();
    }
    void HandleRotation()
    {
        float x = Input.GetAxis("Mouse X");
        lookAngle += x * turnSpeed;
        transform.localRotation = Quaternion.Euler(0, lookAngle, 0);

        float y = Input.GetAxis("Mouse Y");
        tiltAngle -= y * turnSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);
        pivot.localRotation = Quaternion.Euler(tiltAngle, pivotEulers.y, pivotEulers.z);
    }
    void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
    }

}
