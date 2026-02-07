using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileWeaponBehaviour : MonoBehaviour
{

    public WeaponsScriptableObject weaponData;
    
    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Estatus Atuais
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
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

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) //Esquerda
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakebleProps breakeble))
            {
                breakeble.TakeDamage(currentDamage);
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {         currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
