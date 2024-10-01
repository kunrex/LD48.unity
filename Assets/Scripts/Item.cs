using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public enum ItemType
    {
        health,
        damage,
        speed
    }

    public ItemType itemType;

    [Header("Health")]
    public float healthBuff;

    [Header("Speed")]
    public float speedBuff;

    [Header("Damage")]
    public float damageBuff;
}
