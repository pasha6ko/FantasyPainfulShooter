using UnityEngine;
using UnityEngine.UI;

public class ShopsItem : MonoBehaviour
{
    [Header("Shop Components")]
    [SerializeField] private Item item;
    [SerializeField] private CurrencySystem currencySystem;

    [Header("Player Componnets")]
    [SerializeField] private ItemLibrary library;
    [SerializeField] private EXPSystem expSystem;

    public void Buy()
    {
        if (item.itemType == Item.Types.Artifact) return;
        if (!currencySystem.PlusNotMinus(item.itemPrice)) return;
        if (!BuyAbility())
        {
            if (!library.AddItem(item)) return;
        }
        currencySystem.RemoveMoney(item.itemPrice);
    }

    public void Sell()
    {
        foreach (InventorySlot inventorySlot in library.GetSlots())
        {
            if (inventorySlot.item != item) continue;
            inventorySlot.isChoosed = true;

            library.DeleteItem(inventorySlot, inventorySlot.transform.GetChild(0).GetComponent<Image>());
            currencySystem.AddMoney((int)(item.itemPrice * item.itemSellPercent));
            return;
        }
    }

    private bool BuyAbility()
    {
        if (item.itemType == Item.Types.DamageAbility)
        {
            foreach (GunSettings settings in expSystem.abilities.gunSettingsList)
            {
                settings.damage += (int)(0.1f * settings.damage);
            }
            expSystem.abilities.abilitiesCount[expSystem.abilities.closuresList[0]] += 1;
            expSystem.CheckAbilities();
            return true;
        }
        else if (item.itemType == Item.Types.HPAbility)
        {
            expSystem.abilities.playerHp.HP += (int)(0.08f * expSystem.abilities.playerHp.HP);
            print(expSystem.abilities.playerHp.HP);
            expSystem.abilities.abilitiesCount[expSystem.abilities.closuresList[1]] += 1;
            expSystem.CheckAbilities();
            return true;
        }
        return false;
    }
}
