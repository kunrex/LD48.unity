using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    [SerializeField] private AudioSource laser;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetMouseButtonDown(0))
            if (Time.timeSinceLevelLoad >= TimeToNextFire)
            {
                TimeToNextFire = Time.timeSinceLevelLoad + 1 / fireRate;
                Shoot();
            }
    }

    protected override void Shoot()
    {
        muzzleFlash.Play();

        GameObject _projectile = Instantiate(projectile, firePoint.position, Quaternion.identity);

        laser.Play();

        Rigidbody2D rb = _projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        Projectile _Projectile = _projectile.GetComponent<Projectile>();

        if(projectile != null)
            _Projectile.SetDamage(damage);

        _Projectile.SetVel(transform.right);
    }
}
