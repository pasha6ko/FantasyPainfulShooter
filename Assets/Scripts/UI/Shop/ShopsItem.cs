using UnityEngine;
using UnityEngine.UI;

public class ShopsItem : MonoBehaviour
{
    [Header("Shop Components")]
    [SerializeField] private Item item;
    [SerializeField] private CurrencySystem currencySystem;

    [Header("Player Componnets")]
    [SerializeField] private ItemLibrary library;

    private float _percentForSell = 0.8f;

    public void Buy()
    {
        if (!currencySystem.PlusNotMinus(item.itemPrice)) return;
        if (!library.AddItem(item)) return;
        currencySystem.RemoveMoney(item.itemPrice);
    }

    public void Sell()
    {
        foreach (InventorySlot inventorySlot in library.GetSlots())
        {
            if (inventorySlot.item != item) continue;
            inventorySlot.isChoosed = true;

            library.DeleteItem(inventorySlot, inventorySlot.transform.GetChild(0).GetComponent<Image>());
            currencySystem.AddMoney((int)(item.itemPrice * _percentForSell));
            return;
        }
    }
}
