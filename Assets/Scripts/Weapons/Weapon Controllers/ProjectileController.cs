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
        GameObject spawnedProjectile = Instantiate(prefab);
        spawnedProjectile.transform.position = transform.position;
        spawnedProjectile.GetComponent<ProjectileBehaviour>().DirectionChecker(pm.lastMovedVector);     
    }
}
