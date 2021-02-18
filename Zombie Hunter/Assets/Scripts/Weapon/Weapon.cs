using System;
using System.Collections;
using TMPro;
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
    [SerializeField] AmmoType ammoType = AmmoType.Pistol;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canFire = true;

    private void OnEnable()
    {
        canFire = true;
    }

    void Update()
    {
        DisplayAmmo();

        if (CrossPlatformInputManager.GetButtonDown("Fire1") && canFire)
        {
            Shoot();
        }
    }

    private void DisplayAmmo()
    {
        ammoText.text = $"{ammoSlot.GetCurrentAmmo(ammoType)}";
    }

    private void Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) <= 0) return;

        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo(ammoType);

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
