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
    [SerializeField] AudioClip hitSFX = null;
    [SerializeField] AudioClip deathSFX = null;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (hitPoints <= 0) return;

        ProcessHit();
        audioSource.PlayOneShot(hitSFX);
        PlayParticleEffect();
    }

    private void PlayParticleEffect()
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
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        
        Destroy(gameObject);
    }

    private void PlayDeathParticles()
    {
        ParticleSystem deathParticle = Instantiate(deathParticlePrefab, transform.position + deathParticlePosition, Quaternion.identity);
        deathParticle.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration);  // Must destroy game object
    }
}
