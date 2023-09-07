using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLibrary : MonoBehaviour
{
    [Header("Slots Objects")]
    [SerializeField] public List<GameObject> slots;

    public bool AddItem(Item item)
    {
        foreach (GameObject slot in slots)
        {
            InventorySlot itemsSlot = slot.GetComponent<InventorySlot>();
            Image itemsImage = slot.transform.GetChild(0).GetComponent<Image>();
            if (itemsSlot.item != null) continue;

            itemsSlot.item = item;
            itemsImage.sprite = item.itemSprite;
            itemsImage.enabled = true;
            return true;
        }
        return false;
    }

    public void DeleteItems()
    {
        foreach (GameObject slot in slots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();

            if (!inventorySlot.isChoosed) continue;
            inventorySlot.item = null;
            inventorySlot.isChoosed = false;
            inventorySlot.isHighlighted = false;

            Image slotImage = slot.GetComponent<Image>();
            slotImage.color = Color.white;

            Image itemImage = slot.transform.GetChild(0).GetComponent<Image>();
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
    }
}
