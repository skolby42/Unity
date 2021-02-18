using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas damageReceivedCanvas;
    [SerializeField] float impactTime = 0.3f;

    private void Start()
    {
        damageReceivedCanvas.enabled = false;
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    private IEnumerator ShowSplatter()
    {
        damageReceivedCanvas.enabled = true;
        yield return new WaitForSeconds(impactTime);
        damageReceivedCanvas.enabled = false;
    }
}
