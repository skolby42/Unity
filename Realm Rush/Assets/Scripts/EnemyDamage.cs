using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab = null;
    [SerializeField] ParticleSystem deathParticlePrefab = null;
    [SerializeField] Vector3 deathParticlePosition = new Vector3(0f, 0f, 0f);


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
            hitParticlePrefab.Play();
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
        ParticleSystem deathParticle = Instantiate(deathParticlePrefab, transform.position + deathParticlePosition, Quaternion.identity);
        deathParticle.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration);  // Must destroy game object
    }
}
