using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[System.Serializable]
public class FireEvent : UnityEvent<bool, Transform> {}

[RequireComponent(typeof(Weapon))]
public class FireArm : MonoBehaviour
{
    public Animator animator;
    [Header("Ammo Settings")]
    public int ammo = 10;
    public int reserveAmmo = 50;
    public int magazineSize = 10;
    public int ammoPerShot = 1;
    public int ammoLostPerShot = 1;
    [Header("Balance Settings")]
    public float shotDelay = 0f;
    [Tooltip("Time between shots")]
    public float shotCooldown = 0.5f;
    [Tooltip("Time it takes to reload")]
    public float reloadCooldown = 1.5f;
    [Tooltip("Spread radius around target")]
    public float baseSpread = 3f;
    [Tooltip("Spread multiplier over time")]
    public AnimationCurve spreadOverTime;
    public float spreadImprecisionTime;
    public AnimationCurve horizontalSprayPattern;
    public float horizontalSprayMultiplier;
    public AnimationCurve verticalSprayPattern;
    public float verticalSprayMultiplier;
    public float sprayImprecisionTime;
    public float damageFalloff = 0.01f;
    public float weaponRange;
    [Header("Internal")]
    public bool shooting = false;
    public bool reloading = false;
    public float lastShotTime = 0;
    [Header("Events")]
    public FireEvent fireEvent;

    public void Reload()
    {
        if (!reloading && ammo != magazineSize && reserveAmmo > 0) {
            reloading = true;
            animator.SetBool("Reload", true);
            Debug.Log("Reload");
            StartCoroutine(StopReloading());
        }
    }

    IEnumerator StopReloading()
    {
        int oldAmmo = Mathf.Min(0, ammo);

        yield return new WaitForEndOfFrame();
        animator.SetBool("Reload", false);
        yield return new WaitForSeconds(reloadCooldown);
        ammo = Mathf.Min(magazineSize, ammo + reserveAmmo);
        reserveAmmo -= Mathf.Min(magazineSize, ammo - oldAmmo);
        reloading = false;
    }

    public void Fire(bool isFirstFrame, Transform camera)
    {
        
        if (!reloading) {
            if (ammo <= 0) {
                Reload();
                return;
            }
            animator.SetBool("Secondary Fire", true);
            StartCoroutine(WaitForFire(isFirstFrame, camera));
        }
    }

    IEnumerator WaitForFire(bool isFirstFrame, Transform camera)
    {
        yield return new WaitForEndOfFrame();
        animator.SetBool("Secondary Fire", false);
        lastShotTime = Time.time;
        yield return new WaitForSeconds(shotDelay);
        Debug.Log("Fire");
        shooting = true;
        if (ammo != 0)
            ammo -= Mathf.Min(ammoLostPerShot, ammo);
        fireEvent.Invoke(isFirstFrame, camera);
        yield return new WaitForSeconds(shotCooldown - 0.02f);
        if (ammo <= 0)
            Reload();
    }
}
