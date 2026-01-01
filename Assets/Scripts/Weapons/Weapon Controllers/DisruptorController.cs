using UnityEngine;

public class DisruptorController : WeaponController
{
    protected override void Attack()
    {
        base.Attack(); // Reseta o cooldown

        GameObject spawnedDisruptor = Instantiate(weaponData.prefab, transform.position, Quaternion.identity);
        spawnedDisruptor.transform.SetParent(transform); // Usa SetParent em vez de .parent

        // Passa o dano para o disruptor
        DisruptorBehaviour disruptor = spawnedDisruptor.GetComponent<DisruptorBehaviour>();
        if (disruptor != null)
        {
            disruptor.Initialize(weaponData.Damage);
        }
    }
}