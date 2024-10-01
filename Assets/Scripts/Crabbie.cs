using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabbie : Enemy
{
    [SerializeField] private int playerMaskInt;
    [SerializeField] private float jumpForce, minYDistance, time, intensity;
    bool hasAttacked = false, isDead;
    private CrabBossManager crabBossManager;

    [SerializeField] private AudioSource thud;

    // Start is called before the first frame update
    void Start()
    {
        crabBossManager = transform.parent.GetComponent<CrabBossManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void _Attack()
    {
        Attack();
    }

    protected override void Attack()
    {
        Vector2 dir = ((Vector2)player.transform.position - rb.position).normalized;
        rb.AddForce(dir * jumpForce, ForceMode2D.Impulse);
        hasAttacked = true;
    }

    protected override void MoveToPlayer()
    {
        Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
        Vector2 moveTowards = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(moveTowards);
    }

    public override void Die()
    {
        Die(false);
    }

    public void Die(bool collision = false)
    {
        if (isDead) return;

        if(!collision)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isDead = true;

        crabBossManager.ChangeNumber(1);
        crabBossManager.StartCoroutine(crabBossManager.CrabbieAttack());
        animator.SetTrigger("Death");
        Destroy(gameObject, deathTime);
    }

    public void SetJumpForce(float val)
    {
        jumpForce = val;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!hasAttacked || isDead)
            return;

        if(col.gameObject.layer == playerMaskInt)
        {
            HealthScript script = col.gameObject.GetComponent<HealthScript>();
            if(script != null)
            {
                script.TakeDamage(damage);

                Game_Event_Manager.instance.CameraShake(time, intensity);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        thud.Play();

        Die(true);
    }
}
