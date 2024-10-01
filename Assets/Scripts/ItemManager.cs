using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Transform center;
    [SerializeField] private LayerMask itemMask;

    [SerializeField] private CrabBossManager crabBossManager;

    // Start is called before the first frame update
    void Start()
    {
        center = center == null ? transform : center;
    }

    // Update is called once per frame
    void Update()
    {
        bool isClose = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            ItemDisplayer item = collider.GetComponent<ItemDisplayer>();
            if (item != null)
            {
                isClose = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    item.PickUp(gameObject);
                }

                break;
            }

            Treasure treasure = collider.gameObject.GetComponent<Treasure>();

            if(treasure != null && crabBossManager.crabbBossbeaten)
            {
                isClose = true;

                break;
            }

            if(collider.attachedRigidbody != null)
            {
                CrabBoss boss = collider.attachedRigidbody.GetComponent<CrabBoss>();
                if(boss != null)
                {
                    GetComponent<HealthScript>().TakeDamage(101);
                }
            }
        }


        if (!isClose)
            UIManager.instance.EnableInstruction(0);
        else
            UIManager.instance.DisableInstruction(0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(center.position, radius);
    }
}
