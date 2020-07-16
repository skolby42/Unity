using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlesPrefab = null;
    [SerializeField] ParticleSystem deathParticlesPrefab = null;
    [SerializeField] Vector3 deathParticlePosition;


    private void OnParticleCollision(GameObject other)
    {
        if (hitPoints <= 0) return;

        ProcessHit();
        PlayEffect();
    }

    private void PlayEffect()
    {
        if (hitPoints > 0)
        {
            hitParticlesPrefab.Play();
        }
        else
        {
            StartDeathSequence();
        }
    }

    private void ProcessHit()
    {
        hitPoints--;
    }

    private void StartDeathSequence()
    {
        SendMessage("OnEnemyDeath");
        PlayDeathParticles();
        Destroy(gameObject);
    }

    private void PlayDeathParticles()
    {
        ParticleSystem deathParticles = Instantiate(deathParticlesPrefab, transform.position + deathParticlePosition, Quaternion.identity);
        deathParticles.Play();
        Destroy(deathParticles.gameObject, deathParticles.main.duration);  // Must destroy game object
    }
}
