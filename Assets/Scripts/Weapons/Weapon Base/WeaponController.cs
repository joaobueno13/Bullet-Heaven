using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    public int piece;

    protected float currentCooldown;
    protected PlayerMovement pm;

    protected virtual void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();

        if (pm == null)
        {
            Debug.LogError($"WeaponController em {gameObject.name} não encontrou PlayerMovement no pai!");
        }

        currentCooldown = cooldownDuration;
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
        currentCooldown = cooldownDuration;
    }
}