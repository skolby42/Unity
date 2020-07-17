using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float chaseRange = 5f;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRange)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }
}
