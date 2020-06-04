using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireArm))]
public class ProjectileWeapon : FireBehaviour
{
    public float projectileVelocity = 1;
    public GameObject projectile;
    public Transform projectileSpawner;

    public new void Fire(bool isFirstFrame, Transform camera)
    {
        GameObject newProjectile = Instantiate(projectile, projectileSpawner.position, Quaternion.identity);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        
        if (rb) {
            rb.AddForce(new Vector3(camera.forward.x * projectileVelocity,
                camera.forward.y * projectileVelocity,
                camera.forward.z * projectileVelocity));
        }
    }
}
