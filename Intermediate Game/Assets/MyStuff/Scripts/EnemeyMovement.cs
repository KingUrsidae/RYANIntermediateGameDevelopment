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
    private void Awake()
    {
        J_Player = GameObject.FindGameObjectWithTag("Player");
        J_NavAgent = GetComponent<NavMeshAgent>();
        J_Rigidbody = GetComponent<Rigidbody>();
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
        J_EAnimator.SetFloat("Horizontal", J_Rigidbody.position.normalized.x);
        J_EAnimator.SetFloat("Vertical", J_Rigidbody.position.normalized.z);
        
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
