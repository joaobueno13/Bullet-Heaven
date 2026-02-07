using UnityEditor.U2D.Animation;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Current Stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        // Referencia o script PlayerStats do objeto colidido e chama o método TakeDamage para aplicar dano ao jogador
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}
