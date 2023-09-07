using UnityEngine;

public class PickUp : MonoBehaviour, IPickable
{
    [SerializeField] private Item item;

    private ItemLibrary _itemLibrary;
    private string _name;

    public void PickUpItem(Transform player)
    {
        _itemLibrary = player.GetComponent<ItemLibrary>();

        bool isAdded = _itemLibrary.AddItem(item);
        if (!isAdded) return;
        Destroy(gameObject);
    }
}
