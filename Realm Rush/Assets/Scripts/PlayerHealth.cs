using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;

    private void OnTriggerEnter(Collider other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillPlayer();
        }
    }

    private void ProcessHit()
    {
        hitPoints--;

        print("I'm hit!");
    }

    private void KillPlayer()
    {
        print("I'm dead!");
    }
}
