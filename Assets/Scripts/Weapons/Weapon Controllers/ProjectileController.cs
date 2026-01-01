using UnityEngine;

public class ProjectileController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        GameObject spawnedProjectile = Instantiate(weaponData.Prefab);
        spawnedProjectile.transform.position = transform.position;

        // Passa os dados da arma para o projétil
        ProjectileBehaviour projectile = spawnedProjectile.GetComponent<ProjectileBehaviour>();
        projectile.weaponData = weaponData;
        projectile.DirectionChecker(pm.lastMovedVector);
    }
}