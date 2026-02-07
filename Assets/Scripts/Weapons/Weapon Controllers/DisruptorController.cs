using UnityEngine;

public class DisruptorController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab);
        spawnedGarlic.transform.position = transform.position; //Atribua a posição para ser a mesma deste objeto que é o pai do jogador.
        spawnedGarlic.transform.parent = transform;
    }
}
