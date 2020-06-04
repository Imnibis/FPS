using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateAmmo : MonoBehaviour
{
    public TMP_Text currentAmmo;
    public TMP_Text totalAmmo;
    public TMP_Text infinity;
    public GameObject ammoDisplay;
    public Weapon currentWeapon;
    FireArm fireArm;

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null && fireArm != null) {
            ammoDisplay.SetActive(true);
            infinity.gameObject.SetActive(false);
            currentAmmo.SetText(fireArm.ammo.ToString());
            totalAmmo.SetText(fireArm.reserveAmmo.ToString());
        } else if (currentWeapon != null) {
            fireArm = currentWeapon.GetComponent<FireArm>();
            if (fireArm == null) {
                ammoDisplay.SetActive(false);
                infinity.gameObject.SetActive(true);
            }
        }
    }
}
