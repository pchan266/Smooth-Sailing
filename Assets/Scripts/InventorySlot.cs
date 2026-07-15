using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public void SetSlot(ItemData item)
    {
        if (item == null)
        {
            itemImage.enabled = false;
            return;
        }

        itemImage.enabled = true;
        itemImage.sprite = item.image;

    }
}
