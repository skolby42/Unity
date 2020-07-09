using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] [Tooltip("In seconds")] float destroyDelay = 1f;
    [SerializeField] [Tooltip("FX prefab")] GameObject deathFX = null;
    [SerializeField] Vector3 deathFXScale = new Vector3(1f, 1f, 1f);

    bool isDying = false;

    private void Start()
    {
        AddNonTriggerBoxCollider();
    }

    private void OnParticleCollision(GameObject other)
    {
        StartDeathSequence();
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
        Destroy(gameObject, destroyDelay);
    }

    private void InstantiateDeathFX()
    {
        GameObject fx = Instantiate(deathFX, gameObject.transform.position, Quaternion.identity);
        fx.transform.parent = gameObject.transform;
        fx.transform.localScale = deathFXScale;
    }
}
