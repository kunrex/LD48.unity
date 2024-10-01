using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int reflectiveLayer;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private LayerMask layerMask;
    RaycastHit2D hit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == reflectiveLayer || collision.name == "trigger")
        {
            Vector2 normal = hit.normal;
            float angle = Vector2.Angle(rb.velocity.normalized, normal);
            float magnitude = rb.velocity.magnitude;

            Vector2 newVector = Rotate(hit.normal, angle);
            rb.velocity = newVector * magnitude;

            return;
        }
        GameObject _impactEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(_impactEffect, 2f);

        if (collision.attachedRigidbody != null)
        {
            HealthScript health = collision.attachedRigidbody.GetComponent<HealthScript>();
            if (health != null)
                health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void SetVel(Vector2 dir)
    {
        hit = Physics2D.Raycast(transform.position, dir);
    }

    public void SetDamage(float _damage) => damage = _damage;

    public Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
