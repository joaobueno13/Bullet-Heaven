using UnityEngine;

public class ProjectileBehaviour : ProjectileWeaponBehaviour
{
    ProjectileController pc;

    protected override void Start()
    {
        base.Start();
        pc = FindFirstObjectByType<ProjectileController>();
    }

    void Update()
    {
        transform.position += direction * pc.speed * Time.deltaTime;
    }
}
