using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        if (hitPoints > 0)
        {
            hitPoints -= damage;
        }
        else
        {
            DeathHandler deathHandler = FindObjectOfType<DeathHandler>();
            deathHandler.HandleDeath();
        }
    }
}
