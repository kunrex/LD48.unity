using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    [SerializeField] private Item itemToDisplay;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float radius;
    public bool playerClose { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerClose = Physics2D.OverlapCircle(transform.position, radius, playerMask);
    }

    public void PickUp(GameObject _gameObject)
    {
        if (!playerClose)
            return;

        switch(itemToDisplay.itemType)
        {
            case Item.ItemType.health:
                HealthScript health = _gameObject.GetComponent<HealthScript>();
                if(health != null)
                {
                    health.TakeDamage(-itemToDisplay.healthBuff);
                }
                break;
            case Item.ItemType.speed:
                PlayerMovement movement = _gameObject.GetComponent<PlayerMovement>();
                if (movement != null)
                {
                    movement.StartCoroutine(movement.SpeedBuff(itemToDisplay.speedBuff));
                }
                break;
            case Item.ItemType.damage:
                break;
        }

        AudioManager.instance.PlaySound("Button");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
