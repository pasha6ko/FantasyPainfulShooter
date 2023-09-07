using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public bool isChoosed = false;
    [HideInInspector] public bool isHighlighted = false;

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
        if (isHighlighted) return;
        SetSlotColor(highlightColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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
}
