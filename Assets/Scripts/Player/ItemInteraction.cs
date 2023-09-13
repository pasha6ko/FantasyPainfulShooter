using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float pickUpMaxDistance;

    private bool RaycastForItem(out IInteractble pickableItem)
    {
        pickableItem = null;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, maxDistance: pickUpMaxDistance)) return false;
        IInteractble item = hit.transform.GetComponent<IInteractble>();
        if (item == null) return false;
        pickableItem = item;
        return true;
    }
    public void OnInteract()
    {
        IInteractble item;
        if (!RaycastForItem(out item)) return;
        item.Interact(transform);
    }

}
