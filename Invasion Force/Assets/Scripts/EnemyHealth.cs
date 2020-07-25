using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        print($"Ouch, you shot me!  I have {hitPoints} hit points remaining");

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
