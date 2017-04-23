using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Tooltip("Max amount of health (Const)")]
    public uint HealthMax;
    [Tooltip("Current amount of health (Var)")]
    public uint HealthCurrent;

    public void ApplyDamage(Damage damage)
    {
		if (damage.DamageAmount > HealthCurrent) {
			HealthCurrent = 0;
		} else {
			HealthCurrent -= damage.DamageAmount;
		}
    }

    void Update()
    {
        if (HealthCurrent == 0)
        {
            Destroy(gameObject);
        }
    }

}
