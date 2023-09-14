using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemLibrary : MonoBehaviour
{
    [Header("Slots Objects")]
    [SerializeField] public List<GameObject> slots;

    [Header("Items Effects")]
    [SerializeField] private PotionsEffects effects;

    public List<InventorySlot> GetSlots()
    {
        List<InventorySlot> items = new List<InventorySlot>();

        foreach (GameObject slot in slots)
        {
            items.Add(slot.GetComponent<InventorySlot>());
        }

        return items;
    }

    public bool AddItem(Item item)
    {
        foreach (GameObject slot in slots)
        {
            InventorySlot itemsSlot = slot.GetComponent<InventorySlot>();
            if (itemsSlot.item != null) continue;
            UpdateItem(itemsSlot, item, slot.transform.GetChild(0));

            return true;
        }
        return false;
    }

    public void UpdateItem(InventorySlot slot, Item item, Transform imagesSlot)
    {
        Image itemsImage = imagesSlot.GetComponent<Image>();

        slot.item = item;
        itemsImage.sprite = item.itemSprite;
        itemsImage.enabled = true;
    }

    public void DeleteItems(bool isUsing)
    {
        foreach (InventorySlot inventorySlot in GetSlots())
        {
            DeleteItem(inventorySlot, inventorySlot.transform.GetChild(0).GetComponent<Image>(), isUsing);
        }
    }

    public void DeleteItem(InventorySlot inventorySlot, Image itemImage, bool isUsing = false)
    {
        if (!inventorySlot.isChoosed) return;
        if (isUsing) UseItem(inventorySlot.item.itemType);

        inventorySlot.item = null;
        inventorySlot.isChoosed = false;
        inventorySlot.isHighlighted = false;

        Image slotImage = inventorySlot.GetComponent<Image>();
        slotImage.color = Color.white;

        itemImage.sprite = null;
        itemImage.enabled = false;
    }

    private void UseItem(Item.Types type)
    {
        switch (type)
        {
            case Item.Types.Damage:
                StartCoroutine(effects.Damage(2));
                break;

            case Item.Types.Heal:
                effects.Heal(20);
                break;

            case Item.Types.Shield:
                StartCoroutine(effects.MaxArmor(200));
                break;

            case Item.Types.Speed:
                StartCoroutine(effects.Speed(1.5f));
                break;
        }
    }
}
