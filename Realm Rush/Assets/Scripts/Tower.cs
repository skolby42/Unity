using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Waypoint AttachedWaypoint { get; set; }

    [SerializeField] Transform objectToPan = null;
    [SerializeField] float attackRange = 50f;
    [SerializeField] ParticleSystem projectileParticle = null;

    // State
    private Transform targetEnemy = null;

    void Update()
    {
        SetTargetEnemy();
        LookAtEnemy();
        FireAtEnemy();
    }

    private void SetTargetEnemy()
    {
        var enemies = FindObjectsOfType<EnemyMovement>();
        if (enemies.Length == 0) return;

        Transform closestEnemy = enemies[0].transform;
        foreach (var enemy in enemies)
        {
            closestEnemy = GetClosest(closestEnemy.transform, enemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distA = Vector3.Distance(transform.position, transformA.transform.position);
        var distB = Vector3.Distance(transform.position, transformB.transform.position);

        return distA <= distB ? transformA : transformB;
    }

    private bool CanFire()
    {
        if (targetEnemy == null) return false;

        float distance = Vector3.Distance(transform.position, targetEnemy.position);
        return distance <= attackRange;
    }

    private void LookAtEnemy()
    {
        if (targetEnemy == null) return;

        objectToPan.LookAt(targetEnemy);
    }

    private void FireAtEnemy()
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = CanFire();
    }
}
