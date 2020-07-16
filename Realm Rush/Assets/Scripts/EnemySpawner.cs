using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyPrefab = null;
    [SerializeField] [Range(0.1f, 120f)] float secondsBetweenSpawns = 10f;


    void Start()
    {
        StartCoroutine(SpawnEnemyRepeating());
    }

    private IEnumerator SpawnEnemyRepeating()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
