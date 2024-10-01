using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthScript
{
    [SerializeField] private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;

        health = health > maxHealth ? maxHealth : health;

        if (health <= 0)
        {
            Die();
        }
    }


    protected override void Die()
    {
        enemy.Die();
    }
}
