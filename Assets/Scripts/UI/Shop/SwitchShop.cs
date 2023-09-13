using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class SwitchShop : MonoBehaviour, IInteractble
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private GunInteraction gunInteraction;
    [SerializeField] private SwitchInventory inventory;

    [Header("Shop Components")]
    [SerializeField] private GameObject shop;

    public void Interact(Transform transform)
    {
        shop.SetActive(true);
        SetCursor(CursorLockMode.None);
        SwitchMoving(false);
    }

    private void SwitchMoving(bool value)
    {
        playerMovement.enabled = value;
        playerLook.enabled = value;
        gunInteraction.enabled = value;
        inventory.isInventoryOpened = !value;
    }

    private void SetCursor(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }
}
