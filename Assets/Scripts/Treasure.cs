using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private CrabBossManager crabBossManager;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!crabBossManager.crabbBossbeaten)
            return;

        bool isClose = Physics2D.OverlapCircle(transform.position, radius, playerMask);
        if(isClose && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.instance.EndGame(0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
