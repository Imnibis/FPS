using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> {}

public class Damageable : MonoBehaviour
{
    public FloatEvent onDamage;

    public void Damage(float damage)
    {
        onDamage.Invoke(damage);
    }
}
