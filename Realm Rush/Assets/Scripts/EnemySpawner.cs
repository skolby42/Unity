using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyPrefab = null;
    [SerializeField] float secondsBetweenSpawns = 10f;


    void Start()
    {
        StartCoroutine(SpawnEnemyRepeating());
    }

    private IEnumerator SpawnEnemyRepeating()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            Instantiate(enemyPrefab, transform, true);
        }
    }
}
