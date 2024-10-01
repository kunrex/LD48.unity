using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;

    [SerializeField] protected float minAttackDistance, deathTime;
    [SerializeField] protected Animator animator;
    public GameObject player;
    public float damage;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected LayerMask playerMask;
    protected bool isAttacking = false;

    protected abstract void MoveToPlayer();
    protected abstract void Attack();
    public abstract void Die();
}
