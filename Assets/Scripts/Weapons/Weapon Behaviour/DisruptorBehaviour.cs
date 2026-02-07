using System.Collections.Generic;
using UnityEngine;

public class DisruptorBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        TryDamage(col);
    }

    protected void OnTriggerStay2D(Collider2D col)
    {
        // Garante que colisões "na borda" também sejam detectadas
        TryDamage(col);
    }

    protected void OnTriggerExit2D(Collider2D col)
    {
        if (col != null && markedEnemies.Contains(col.gameObject))
        {
            markedEnemies.Remove(col.gameObject);
        }
    }

    private void TryDamage(Collider2D col)
    {
        if (col == null)
            return;

        var go = col.gameObject;
        if (markedEnemies.Contains(go))
            return;

        if (col.CompareTag("Enemy"))
        {
            if (go.TryGetComponent(out EnemyStats enemy))
            {
                enemy.TakeDamage(currentDamage);
                markedEnemies.Add(go);  // Marca o inimigo
            }
        }
        else if (col.CompareTag("Prop"))
        {
            if (go.TryGetComponent(out BreakebleProps breakeble))
            {
                breakeble.TakeDamage(currentDamage);
                markedEnemies.Add(go);  // Marca a prop
            }
        }
    }
}