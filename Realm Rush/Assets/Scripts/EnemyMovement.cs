﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
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
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnEnemyDeath()
    {
        StopCoroutine(pathCoroutine);
    }
}
