using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class EnemeyMovement : MonoBehaviour
{
    [Header("AI")]
    public float m_CloseDistance;
    private GameObject J_Player;
    private NavMeshAgent J_NavAgent;
    private Rigidbody J_Rigidbody;
    private bool J_Follow;

    [Header("Animation")]
    Animator J_EAnimator;
    private float forwardDirection;
    private float sideDirection;
    private Transform J_Enemey;
    Vector3 EnemeyMove;
    private void Awake()
    {
        J_Player = GameObject.FindGameObjectWithTag("Player");
        J_NavAgent = GetComponent<NavMeshAgent>();
        J_Rigidbody = GetComponent<Rigidbody>();
        J_Enemey = gameObject.GetComponent<Transform>();
        J_Follow = false;
    }

    private void OnEnable()
    {
        J_EAnimator = gameObject.GetComponent<Animator>();
        J_Rigidbody.isKinematic = false;
    }
    private void OnDisable()
    {
        J_Rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            J_Follow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            J_Follow = false;
        }
    }
    private void Update()
    {
        EnemeyMove = 1 * J_Enemey.forward + J_Enemey.right * 1;
        EnemeyMove.Normalize();
        forwardDirection = EnemeyMove.z;//GetAxis("Vertical");
        sideDirection = EnemeyMove.x; //GetAxis("Horizontal");
        J_EAnimator.SetFloat("Horizontal", forwardDirection);
        J_EAnimator.SetFloat("Vertical", sideDirection);
        if (J_Follow == false)
            return;
        float distance = (J_Player.transform.position - transform.position).magnitude;
        if (distance > m_CloseDistance)
        {
            J_NavAgent.SetDestination(J_Player.transform.position);
            J_NavAgent.isStopped = false;
        }
        else
        {
            J_NavAgent.isStopped = true;
        }
    }
}
