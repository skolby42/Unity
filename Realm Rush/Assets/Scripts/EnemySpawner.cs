using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyPrefab = null;
    [SerializeField] [Range(0.1f, 120f)] float secondsBetweenSpawns = 10f;
    [SerializeField] Transform enemyParentTransform = null;

    void Start()
    {
        StartCoroutine(SpawnEnemyRepeating());
    }

    private IEnumerator SpawnEnemyRepeating()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyParentTransform);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
