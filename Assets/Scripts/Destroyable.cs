using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float scale;

    public void Destroy()
    {
        GameObject _explsosion = Instantiate(explosion, transform.position, Quaternion.identity);
        _explsosion.transform.localScale = new Vector3(scale, scale); 

        Destroy(gameObject);
        Destroy(_explsosion, 1f);
    }
}
