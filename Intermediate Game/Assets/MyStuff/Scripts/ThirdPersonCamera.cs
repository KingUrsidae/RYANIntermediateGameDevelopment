using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

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
    // Gun stuff
    public Transform J_Gun;
    private LayerMask J_LayerMask;

    // Start is called before the first frame update
    void Start()
    {

        if (lockCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        pivot = GetComponentInChildren<Camera>().transform.parent;
        pivotEulers = pivot.rotation.eulerAngles;
        J_LayerMask = LayerMask.GetMask("Cursor");
    }    
    // Update is called once per frame
    private void Update()
    {
        Aim();
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
    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, J_LayerMask))
        {
            J_Gun.LookAt(hit.point);
        }
    }
    void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
    }

}
