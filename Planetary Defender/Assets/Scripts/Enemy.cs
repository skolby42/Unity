using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] [Tooltip("In seconds")] float destroyDelay = 0.5f;
    [SerializeField] [Tooltip("FX prefab")] GameObject deathFX = null;
    [SerializeField] Vector3 deathFXScale = new Vector3(1f, 1f, 1f);
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hitsRemaining = 2;

    bool isDying = false;

    ScoreBoard scoreBoard;

    private void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitsRemaining <= 0)
        {
            StartDeathSequence();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hitsRemaining--;
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }  

    private void StartDeathSequence()
    {
        if (isDying) return;
        isDying = true;

        InstantiateDeathFX();
        DisableCollider();
        Destroy(gameObject, destroyDelay);
    }

    private void DisableCollider()
    {
        Collider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = false;
    }

    private void InstantiateDeathFX()
    {
        GameObject fx = Instantiate(deathFX, gameObject.transform.position, Quaternion.identity);
        fx.transform.parent = gameObject.transform;
        fx.transform.localScale = deathFXScale;
    }
}
