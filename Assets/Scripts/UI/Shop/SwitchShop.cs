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
    [SerializeField] private EXPSystem exp;

    public void Interact(Transform transform)
    {
        exp.CheckAbilities();
        LockGuns(inventory.isInventoryOpened);
        if (inventory.isInventoryOpened) return;
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

    private void LockGuns(bool value)
    {
        foreach (Gun gun in inventory.guns)
        {
            gun.isLocked = !value;
            print(gun.isLocked);
        }
    }

    private void SetCursor(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }
}
