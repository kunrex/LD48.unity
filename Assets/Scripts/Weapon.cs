using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float bulletForce;
    [SerializeField] protected ParticleSystem muzzleFlash;

    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Transform firePoint;
    protected float TimeToNextFire;

    protected enum FireType
    {
        single,
        multi
    }
    protected FireType fireType;


    protected abstract void Shoot();
}
