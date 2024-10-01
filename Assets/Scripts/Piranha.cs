using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : Enemy
{
    [SerializeField] private PiranhaManager piranhaManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        piranhaManager = piranhaManager == null ? transform.parent.GetComponent<PiranhaManager>() : piranhaManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        isAttacking = Physics2D.OverlapCircle(transform.position, minAttackDistance, playerMask);

        if (!isAttacking)
        {
            MoveToPlayer();
        }
        else
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public override void Die()
    {
        animator.SetTrigger("Death");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        piranhaManager.ChangeNumber(1);

        Destroy(gameObject, deathTime);
    }

    protected override void MoveToPlayer()
    {
        Vector2 moveTowards = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(moveTowards);

        if (rb.position.x > player.transform.position.x)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if(rb.position.x < player.transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, minAttackDistance);
    }
}
