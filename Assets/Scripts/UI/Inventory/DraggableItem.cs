using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Item Components")]
    [SerializeField] public Image image;

    [HideInInspector] public Transform parentBeforeDrag, parentInDrag, parentAfterDrag;

    private void Start()
    {
        parentBeforeDrag = transform.parent;
        parentInDrag = transform.parent.parent.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(parentInDrag);
        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentBeforeDrag);

        image.raycastTarget = true;
    }
}
