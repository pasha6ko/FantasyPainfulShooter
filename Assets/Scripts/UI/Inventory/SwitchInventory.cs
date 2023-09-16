using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchInventory : MonoBehaviour
{
    [Header("Guns Components")]
    [SerializeField] public List<Gun> guns;

    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private GunInteraction gunInteraction;

    [Header("Inventory Components")]
    [SerializeField] private GameObject bigInventory;
    [SerializeField] private GameObject shop;

    private ItemLibrary itemLibrary;
    private List<GameObject> _slots;
    private bool _isInventoryOpened = false;

    public bool isInventoryOpened
    {
        get => _isInventoryOpened;
        set
        {
            _isInventoryOpened = value;
        }
    }

    private void Start()
    {
        SetCursor();

        itemLibrary = GetComponent<ItemLibrary>();
        _slots = itemLibrary.slots;
    }

    public void OnInventoryClick()
    {
        _isInventoryOpened = !_isInventoryOpened;

        SwitchMoving();
        LockGuns();
        InventorySwitch();
        SetCursor();
        ResetSlotsColor();
    }

    public void OnDeleteItems()
    {
        if (!_isInventoryOpened) return;
        itemLibrary.DeleteItems(false);
    }

    public void OnUseItems()
    {
        if (!_isInventoryOpened) return;
        itemLibrary.DeleteItems(true);
    }

    private void SetCursor()
    {
        Cursor.lockState = CursorLockMode.None;

        if (_isInventoryOpened) return;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SwitchMoving()
    {
        playerMovement.enabled = !_isInventoryOpened;
        playerLook.enabled = !_isInventoryOpened;
        gunInteraction.enabled = !_isInventoryOpened;

        shop.SetActive(false);
    }

    private void LockGuns()
    {
        foreach (Gun gun in guns)
        {
            gun.isLocked = _isInventoryOpened;
        }
    }

    private void InventorySwitch()
    {
        bigInventory.SetActive(_isInventoryOpened);
    }

    private void ResetSlotsColor()
    {
        foreach (GameObject slot in _slots)
        {
            slot.GetComponent<Image>().color = Color.white;

            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            inventorySlot.isChoosed = false;
            inventorySlot.isHighlighted = false;
        }
    }
}
