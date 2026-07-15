using System.Collections.Generic;
using UnityEngine;

public class InventorySlotManager : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private List<ItemData> items;

    void Start()
    {
        Populate();
    }

    void Populate()
    {
        foreach (var item in items)
        {
            InventorySlot newSlot = Instantiate(slotPrefab, gridParent);
            newSlot.SetSlot(item);
        }
    }
}
