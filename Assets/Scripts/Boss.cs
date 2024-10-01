using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    [SerializeField] protected int playerMask;
    public GameObject player;

    public abstract void Attack1();

    public abstract void Attack2();

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == playerMask)
        {
            HealthScript health = collision.collider.attachedRigidbody.GetComponent<HealthScript>();
            if (health != null)
                health.TakeDamage(101f);
        }
    }
}
