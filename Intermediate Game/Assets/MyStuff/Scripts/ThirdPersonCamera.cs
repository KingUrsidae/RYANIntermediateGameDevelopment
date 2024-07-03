using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
/// <summary>
/// This is the camera scrip and it also reffrences the game manager so that the cursor can change sensitivity and be unlocked and locked.
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public GameManager gameManager;

    [Header("Follow the target")]
    public Transform target;
    public float moveSpeed = 7f;

    [Header("Rotate camrea")]
    public float turnSpeed;
    public float tiltMax = 75f;
    public float tiltMin = 45f;
    private Transform pivot;
    private float tiltAngle;
    private float lookAngle;
    private Vector3 pivotEulers;

    // Start is called before the first frame update
    void Awake()
    {
        lockCursor = false;
        pivot = GetComponentInChildren<Camera>().transform.parent;
        pivotEulers = pivot.rotation.eulerAngles;
    }    
    // Update is called once per frame
    private void Update()
    {
        HandleRotation();
        CheckCursor();
    }
    void HandleRotation()
    {
        turnSpeed = gameManager.J_SensValue;

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
    void CheckCursor()
    {
        if (lockCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (lockCursor == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void LockCursor()
    {
        lockCursor = true;
    }
    public void UnLockCursor()
    {
        lockCursor = false;
    }
}
