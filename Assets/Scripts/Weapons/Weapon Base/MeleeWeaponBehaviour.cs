using UnityEngine;
using UnityEngine.Rendering;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponsScriptableObject weaponData;

    public float destroyAfterSeconds;

    //Estatus atuais
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
            {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakebleProps breakeble))
            {
                breakeble.TakeDamage(currentDamage);
            }
        }
    }
}
