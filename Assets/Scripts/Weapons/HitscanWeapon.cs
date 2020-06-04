using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(FireArm))]
public class HitscanWeapon : FireBehaviour
{
    float firstShotTime;
    FireArm fireArm;
    public GameObject testPrefab;
    public float spreadDivider;

    private void Start()
    {
        fireArm = GetComponent<FireArm>();
    }

    private void Update()
    {
        
    }

    float CalculateDamage()
    {
        return GetComponent<Weapon>().baseDamage;
    }

    void SendRay(Vector3 origin, Vector3 direction)
    {
        Damageable damageableObject;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, fireArm.weaponRange)) {
            Debug.Log("Hit!");
            damageableObject = hit.collider.GetComponent<Damageable>();
            Instantiate(testPrefab, hit.point, Quaternion.identity);
            if (damageableObject != null) {
                damageableObject.Damage(CalculateDamage());
                Debug.Log("Damageable!");
            }
        }
    }

    Vector3 CalculateSpread(Vector3 direction)
    {
        float maxSpread = fireArm.baseSpread;
        float animationTime = Time.time - firstShotTime /
            fireArm.spreadImprecisionTime;

        maxSpread += fireArm.spreadOverTime.Evaluate(animationTime);
        maxSpread /= spreadDivider;
        float horizontalSpread = Random.Range(-maxSpread, maxSpread);
        float verticalSpread = Random.Range(-maxSpread, maxSpread);
        Vector3 newRotation = direction + new Vector3(verticalSpread, horizontalSpread, 0);
        newRotation = new Vector3(newRotation.x >= 180 ? newRotation.x - 360 : newRotation.x,
            newRotation.y >= 180 ? newRotation.y - 360 : newRotation.y,
            newRotation.z >= 180 ? newRotation.z - 360 : newRotation.z);
        return newRotation;
    }

    public void Fire(bool isFirstFrame, Transform camera)
    {
        if (isFirstFrame)
            firstShotTime = Time.time;
        for(int i = 0; i < fireArm.ammoPerShot; i++) {
            SendRay(camera.position, CalculateSpread(camera.forward));
        }
    }
}
