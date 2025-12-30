using UnityEngine;

public class DisruptorBehaviour : MeleeWeaponBehaviour
{
    private float damage;

    public void Initialize(float weaponDamage)
    {
        this.damage = weaponDamage;
    }

    public float GetDamage() => damage;
}