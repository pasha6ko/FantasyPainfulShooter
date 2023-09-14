using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemsVoid : MonoBehaviour, IDropHandler
{
    [Header("Items Components")]
    [SerializeField] private ItemLibrary itemLibrary;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        Transform droppedItemParent = droppedItem.GetComponent<DraggableItem>().parentBeforeDrag;
        InventorySlot slot = droppedItemParent.GetComponent<InventorySlot>();
        Image image = droppedItem.GetComponent<Image>();

        droppedItemParent.GetComponent<InventorySlot>().isChoosed = true;
        itemLibrary.DeleteItem(slot, image);
    }
}
