using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] ParticleSystem goalParticlePrefab = null;
    [SerializeField] Vector3 goalParticlePosition = new Vector3(0f, 0f, 0f);

    Coroutine pathCoroutine;

    void Start()
    {
        var pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        pathCoroutine = StartCoroutine(FollowPath(path));
    }

    private IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;

            //if (waypoint == path[path.Count - 1])
            //{
            //    break;
            //}

            yield return new WaitForSeconds(movementPeriod);
        }

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        var goalParticle = Instantiate(goalParticlePrefab, transform.position + goalParticlePosition, Quaternion.identity);
        goalParticle.Play();
        Destroy(goalParticle.gameObject, goalParticle.main.duration);

        Destroy(gameObject);
    }

    private void OnEnemyDeath()
    {
        StopCoroutine(pathCoroutine);
    }
}
