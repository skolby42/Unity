using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera = null;
    [SerializeField] float range = 100f;
    [SerializeField] float damagePerHit = 10f;
    [SerializeField] float fireDelay = 0.5f;
    [SerializeField] ParticleSystem muzzleFlash = null;
    [SerializeField] GameObject hitEffectPrefab = null;
    [SerializeField] Ammo ammoSlot = null;

    bool canFire = true;

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && canFire)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (ammoSlot.GetCurrentAmmo() <= 0) return;

        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo();

        canFire = false;
        StartCoroutine(ResetFiring());
    }

    private void PlayMuzzleFlash()
    {
        if (muzzleFlash == null) return;
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hit, range))
        {
            CreateHitEffect(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damagePerHit);
        }
    }

    private void CreateHitEffect(RaycastHit hit)
    {
        GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitEffect, 1f);
    }

    private IEnumerator ResetFiring()
    {
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }
}
