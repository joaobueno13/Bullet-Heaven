using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponsScriptableObject weaponData;
    protected float currentCooldown;

    protected PlayerMovement pm;

    protected virtual void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();

        if (pm == null)
        {
            Debug.LogError($"WeaponController em {gameObject.name} não encontrou PlayerMovement no pai!");
        }

        // Inicializa em 0 para permitir o primeiro ataque imediatamente
        currentCooldown = 0f;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        // Reseta o cooldown após atacar
        currentCooldown = weaponData.CooldownDuration;
    }
}