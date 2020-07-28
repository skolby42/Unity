using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    private PlayerHealth target;

    NavMeshAgent navMeshAgent;
    bool isProvoked;
    bool isDead;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (isDead) return;

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (Vector3.Distance(target.transform.position, transform.position) <= chaseRange)
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    public void OnEnemyDeath()
    {
        isDead = true;
        GetComponent<Animator>().SetTrigger("Dead");
        navMeshAgent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void EngageTarget()
    {
        FaceTarget();

        if (Vector3.Distance(target.transform.position, transform.position) >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();    
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetBool("Move", true);
        
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
