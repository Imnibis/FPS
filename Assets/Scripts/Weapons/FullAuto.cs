using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAuto : MonoBehaviour
{
    FireArm fireArm;

    private void Start()
    {
        fireArm = GetComponent<FireArm>();
    }

    public void Reload()
    {
        fireArm.Reload();
    }

    public void Fire(bool isFirstFrame, Transform camera)
    {
        if (Time.time - fireArm.lastShotTime >=
            fireArm.shotDelay + fireArm.shotCooldown) {
            fireArm.Fire(isFirstFrame, camera);
            StartCoroutine(StopShooting());
        }
    }

    IEnumerator StopShooting()
    {
        yield return 0;
        fireArm.shooting = false;
    }
}
