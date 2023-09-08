using UnityEngine;

interface IPickable
{
    public void PickUpItem(Transform player);
}

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public enum Types
    {
        Ammo,
        Artifact,
        Damage,
        Heal,
        Shield,
        Speed,
    }

    public string itemName;
    public Sprite itemSprite;
    public int itemPrice;
    public int itemSale;
    public Types itemType;
}
