using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    [Header("Color Settings")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;

    private void Start()
    {
        SetSlotColor(normalColor);
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
