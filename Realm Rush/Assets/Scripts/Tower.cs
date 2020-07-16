using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan = null;
    [SerializeField] Transform targetEnemy = null;
    [SerializeField] float attackRange = 50f;
    [SerializeField] ParticleSystem projectileParticle = null;

    void Update()
    {
        LookAtEnemy();
        FireAtEnemy();
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
