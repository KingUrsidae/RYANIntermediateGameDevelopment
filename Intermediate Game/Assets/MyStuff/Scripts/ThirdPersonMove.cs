using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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

    [Header("Dash")]
    public Transform orientation;
    public float dashForce;
    public float dashDuration;
    public float dashCd;
    public float dashSpeed;
    public bool canDash;
    public float dashCdTimer;
    public KeyCode dashKey = KeyCode.F;


    // Start is called before the first frame update
    void Start()
    {
        J_Animator = gameObject.GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        canDash = true;
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
        CheckDash();
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
    }
    void AnimationStuff()
    {
        J_Animator.SetFloat("Horizontal", sideDirection);
        J_Animator.SetFloat("Vertical", forwardDirection);
    }
    void CheckDash()
    {
        if (canDash == true)
        {
            if (Input.GetKeyDown(dashKey))
            {
                timeController.DoSlowdown();
                DoDash();
            }
        }
        if (dashCdTimer >= 0)
        {
            dashCdTimer -= Time.unscaledDeltaTime;
            canDash = false;
        }
        if (dashCdTimer <= 0)
        {
            canDash = true;
        }
    }
    private void DoDash()
    {
        if (dashCdTimer > 0)
            return;
        else
            dashCdTimer = dashCd;
        Vector3 forceToApply = orientation.forward * dashForce;
        cc.Move(forceToApply);
        Invoke(nameof(ResetDash), dashDuration);
    }
    private void ResetDash()
    {
        canDash = false; 
    }
    /*Reffrences
    
    */
}
