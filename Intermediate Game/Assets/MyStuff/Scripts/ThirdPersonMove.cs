using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMove : MonoBehaviour
{
    // Inputs
    float forwardDirection;
    float sideDirection;
    // Players rotation to the cam
    public Transform cam;
    Vector3 camForward;
    public float rotationSpeed = 180;
    Vector3 move;
    // Movement
    CharacterController cc;
    public float moveSpeed = 4;
    bool isBulletTimeReady;
    //jumping and gravity 
    public float jumpSpeed = 20.0f;
    public float gravityMultiplier = 3;
    float verticalVelocity = 0;

    public TimeController timeController;

    // Start is called before the first frame update
    void Start()
    {

        cc = GetComponent<CharacterController>();
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        Rotation();
        Movement(); 
    }

    void Inputs()
    {
        forwardDirection = Input.GetAxis("Vertical");
        sideDirection = Input.GetAxis("Horizontal");
    }

    void Rotation()
    {
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1).normalized);
        move = forwardDirection * camForward + sideDirection * cam.right;

        move.Normalize();
        move = transform.InverseTransformDirection(move);

        float turnAmount = Mathf.Atan2(move.x, move.z);
        transform.Rotate(0, turnAmount * rotationSpeed * Time.deltaTime, 0);
    }

    void Movement()
    {
        Vector3 direction = transform.forward * move.z * moveSpeed;
        
        if (cc.isGrounded)
        {
            verticalVelocity = 0;
        }
        verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }
        direction.y = verticalVelocity;

        cc.Move(direction * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F) /*&& isBulletTimeReady is true*/)
        {
            timeController.DoSlowdown();
        }
        
    }
}
