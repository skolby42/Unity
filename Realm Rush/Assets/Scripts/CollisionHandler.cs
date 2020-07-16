using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] float destroyDelay = 1f;
    [SerializeField] Vector3 deathFXScale = new Vector3(1f, 1f, 1f);
    [SerializeField] Vector3 hitFXScale = new Vector3(1f, 1f, 1f);
    [SerializeField] GameObject hitFX = null;
    [SerializeField] GameObject deathFX = null;


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
            InstantiateHitFX();
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
        InstantiateDeathFX();
        Destroy(gameObject, destroyDelay);
    }

    private void InstantiateDeathFX()
    {
        Transform bodyTransform = transform.Find("Body");
        GameObject fx = Instantiate(deathFX, bodyTransform.position, Quaternion.identity);
        fx.transform.parent = bodyTransform;
        fx.transform.localScale = deathFXScale;
    }

    private void InstantiateHitFX()
    {
        Transform bodyTransform = transform.Find("Body");
        GameObject fx = Instantiate(hitFX, bodyTransform.position, Quaternion.identity);
        fx.transform.parent = bodyTransform;
        fx.transform.localScale = hitFXScale;
    }
}
