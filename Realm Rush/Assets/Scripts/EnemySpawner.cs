using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyPrefab = null;
    [SerializeField] [Range(0.1f, 120f)] float secondsBetweenSpawns = 10f;
    [SerializeField] Transform enemyParentTransform = null;
    [SerializeField] Text spawnedEnemiesText = null;

    private int score;

    void Start()
    {
        spawnedEnemiesText.text = score.ToString();
        StartCoroutine(SpawnEnemyRepeating());
    }

    private IEnumerator SpawnEnemyRepeating()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyParentTransform);
            AddScore();
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void AddScore()
    {
        score++;
        spawnedEnemiesText.text = score.ToString();
    }
}
