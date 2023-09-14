using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDropHandler
{
    [HideInInspector] public bool isChoosed = false;
    [HideInInspector] public bool isHighlighted = false;

    [Header("Description Block Componnets")]
    [SerializeField] private GameObject descriptionBlock;
    [SerializeField] private Image descriptionImage;
    [SerializeField] private TextMeshProUGUI descriptionTitle;
    [SerializeField] private TextMeshProUGUI descriptionDescription;

    [Header("Slot Component")]
    [SerializeField] public Item item;
    [SerializeField] private Image slot;
    [SerializeField] private ItemLibrary itemLibrary;

    [Header("Color Settings")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;

    private void Start()
    {
        SetSlotColor(normalColor);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        Item temporaryThisItem = item;

        Transform droppedItemParent = droppedItem.GetComponent<DraggableItem>().parentBeforeDrag;
        Item temporaryOtherItem = droppedItemParent.GetComponent<InventorySlot>().item;

        if (item != null)
        {
            itemLibrary.UpdateItem(droppedItemParent.GetComponent<InventorySlot>(), temporaryThisItem, droppedItem.transform);
            itemLibrary.UpdateItem(this, temporaryOtherItem, this.transform.GetChild(0));
        }
        else
        {
            itemLibrary.UpdateItem(this, temporaryOtherItem, this.transform.GetChild(0));

            droppedItemParent.GetComponent<InventorySlot>().isChoosed = true;
            itemLibrary.DeleteItem(droppedItemParent.GetComponent<InventorySlot>(), droppedItem.GetComponent<Image>());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionBlock.SetActive(true);
            SetDescription();
        }

        if (isHighlighted) return;
        SetSlotColor(highlightColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBlock.SetActive(false);

        if (isHighlighted) return;
        SetSlotColor(normalColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isHighlighted = !isHighlighted;
        isChoosed = !isChoosed;

        SetSlotColor(normalColor);

        if (!isHighlighted) return;
        SetSlotColor(highlightColor);
    }

    private void SetSlotColor(Color color)
    {
        if (slot == null) return;
        slot.color = color;
    }

    private void SetDescription()
    {
        descriptionImage.sprite = item.itemSprite;
        descriptionTitle.text = item.itemName;
        descriptionDescription.text = item.itemDescription;
    }
}
