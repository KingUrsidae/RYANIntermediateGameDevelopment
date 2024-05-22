using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMove : MonoBehaviour
{
    //Inputs
    float forwardDirection;
    float sideDirection;

    [Header("Camera")]
    public Transform cam;
    Vector3 camForward;
    public float rotationSpeed = 180;
    Vector3 move;

    [Header("Movement")]
    CharacterController cc;
    public float moveSpeed = 4;
    public KeyCode JumpKey = KeyCode.Space;

    [Header("Jumping & Gravity")]
    public float jumpSpeed = 20.0f;
    public float gravityMultiplier = 3;
    float verticalVelocity = 0;

    [Header("Time")]
    public TimeController timeController;

    [Header("Animation")]
    Animator J_Animator;
    //float J_HorizontalMovement;
    //float J_VerticalMovement;
    [Header("Dash/Dive")]

    public Transform orientation;
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;
    public float dashCd;
    public float dashSpeed;
    private bool dashing;
    private float dashCdTimer;
    public KeyCode dashKey = KeyCode.F;

    // Start is called before the first frame update
    void Start()
    {
        J_Animator = gameObject.GetComponent<Animator>();
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
        AnimationStuff();
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

        if (Input.GetKeyDown(JumpKey) && cc.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }
        direction.y = verticalVelocity;

        cc.Move(direction * Time.deltaTime);

        if (Input.GetKeyDown(dashKey))
        {
            timeController.DoSlowdown();
            Dash();
        }
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
            
    }
    void AnimationStuff()
    {
        //J_HorizontalMovement = Input.GetAxis("Horizontal");
        J_Animator.SetFloat("Horizontal", sideDirection);
        //J_HorizontalMovement = Input.GetAxis("Vertical");
        J_Animator.SetFloat("Vertical", forwardDirection);
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        cc.Move(forceToApply);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        dashing = false; 
    }


    /*Reffrences
    
    */
}
