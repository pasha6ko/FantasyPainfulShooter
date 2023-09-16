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
        DamageAbility,
        Heal,
        HPAbility,
        Shield,
        Speed,
    }

    public string itemName;
    public Sprite itemSprite;
    public int itemPrice;
    [Range(0, 1)] public float itemSellPercent = 0.8f;
    public Types itemType;
    public bool isUseable = true;
    [TextArea] public string itemDescription;
}
