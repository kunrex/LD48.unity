using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rcok : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float damage, scale, time, intensity;
    [SerializeField] private GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.name == "trigger")
            return;

        HealthScript health = collision.gameObject.GetComponent<HealthScript>();
        if (health != null)
        {
            health.TakeDamage(damage);

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.AddForce(GetComponent<Rigidbody2D>().velocity / 2);
        }

        Game_Event_Manager.instance.CameraShake(time, intensity);

        GameObject _explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        _explosion.transform.localScale = new Vector3(scale, scale);

        Destroy(gameObject);
        Destroy(_explosion, 1f);
    }
}
