using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] Text HealthText = null;
    [SerializeField] AudioClip playerDamageSFX = null;

    private void Start()
    {
        HealthText.text = hitPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        ProcessHit();

        GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);

        if (hitPoints <= 0)
        {
            KillPlayer();
        }
    }

    private void ProcessHit()
    {
        if (hitPoints <= 0) return;

        hitPoints--;

        HealthText.text = hitPoints.ToString();
    }

    private void KillPlayer()
    {
        //print("I'm dead!");
    }
}
