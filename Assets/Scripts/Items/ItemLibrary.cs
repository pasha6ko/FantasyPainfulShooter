using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLibrary : MonoBehaviour
{
    [Header("Slots Objects")]
    [SerializeField] public List<GameObject> slots;

    [Header("Items Effects")]
    [SerializeField] private PotionsEffects effects;

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

    public void DeleteItems(bool isUsing)
    {
        foreach (GameObject slot in slots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();

            if (!inventorySlot.isChoosed) continue;
            if (isUsing) UseItem(inventorySlot.item.itemType);

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
