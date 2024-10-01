using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health { get; protected set; }
    [SerializeField] protected float maxHealth = 100, scale;
    [SerializeField] private GameObject explosion;
    private bool isDead;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
        UIManager.instance.SetPlayerHealth(maxHealth, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        health = health > maxHealth ? maxHealth : health;

        if(health <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }

        UIManager.instance.SetPlayerHealth(health);
    }

    protected virtual void Die()
    {
        GameObject _explosion = Instantiate(explosion, transform.position, Quaternion.identity);

        _explosion.transform.localScale = new Vector3(scale, scale);

        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);

        UIManager.instance.EndGame(1);
    }

}
