using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float pickUpMaxDistance;

    private void FixedUpdate()
    {
        IPickable item;
        if (!RaycastForItem(out item)) return;

    }

    private bool RaycastForItem(out IPickable pickableItem)
    {
        pickableItem = null;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, maxDistance: pickUpMaxDistance)) return false;
        IPickable item = hit.transform.GetComponent<IPickable>();
        if (item == null) return false;
        pickableItem = item;
        return true;
    }
    public void OnInteract()
    {
        IPickable item;
        if (!RaycastForItem(out item)) return;
        item.PickUpItem(transform);
    }

}
