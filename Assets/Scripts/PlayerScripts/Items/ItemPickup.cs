using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item to be picked up")]
    public GameObject itemData;

    [Header("Transform of parent")]
    [SerializeField] private Transform itemPlacement;

    public void addItem()
    {

    }
}
