using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : HealthScript
{
    [SerializeField] private float angryHealth, angryLength, deathAnimationTime;
    [HideInInspector] public bool raged = false, isInvincible;
    [SerializeField] private Animator animator;
    [SerializeField] private CrabBoss boss;

    [SerializeField] private AudioSource deathSound;
    [SerializeField] private CrabBossManager crabBossManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        health = maxHealth;
        UIManager.instance.SetBossHealth(maxHealth, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(float damage)
    {
        if (isInvincible) return;

        health -= damage;

        health = health > maxHealth ? maxHealth : health;

        if (health <= 0)
        {
            Die();
        }

        if (health <= angryHealth && !raged)
        {
            if (boss.isGrounded)
            {
                raged = true;
                animator.SetBool("Angry", true);

                isInvincible = true;
                StartCoroutine(Reset());
            }
            else
            {
                raged = true;
                isInvincible = true;

                StartCoroutine(StartAttackWhenGrounded());
            }
        }

        UIManager.instance.SetBossHealth(health);
    }

    public void Rage()
    {
        if (boss.isGrounded)
        {
            raged = true;
            animator.SetBool("Angry", true);

            isInvincible = true;
            StartCoroutine(Reset());
        }
        else
        {
            raged = true;
            isInvincible = true;

            StartCoroutine(StartAttackWhenGrounded());
        }
    }

    IEnumerator Reset()
    {
        boss.PlayRoarSound();
        yield return new WaitForSeconds(angryLength);

        isInvincible = false;
    }

    IEnumerator StartAttackWhenGrounded()
    {
        while (!boss.isGrounded)
            yield return null;

        animator.SetBool("Angry", true);
        yield return new WaitForSeconds(angryLength);
        boss.PlayRoarSound();

        isInvincible = false;
    }

    protected override void Die()
    {
        animator.SetTrigger("Death");
        crabBossManager.DeadBoss();

        deathSound.Play();

        Destroy(gameObject, deathAnimationTime);
    }
}
