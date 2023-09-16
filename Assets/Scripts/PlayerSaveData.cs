using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveData : MonoBehaviour
{
    public static PlayerSaveData Instance;

    [SerializeField] private PlayerHp hp;
    [SerializeField] private EXPSystem xp;
    [SerializeField] private ItemLibrary library;
    [SerializeField] private List<Item> allItems = new List<Item>();

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        LoadInventory();
    }

    public void SaveData()
    {
        SaveEXP();
        SaveHp();
        SaveInventory();
        SaveLocation();
        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        LoadEXP();
        LoadHp();
        LoadInventory();
    }
    public string GetSavedLocation()
    {
        return PlayerPrefs.GetString("Location");
    }

    private void SaveEXP()
    {
        PlayerPrefs.SetFloat("CurrentXP", xp.system.currentValue);
        PlayerPrefs.SetInt("LevelXP", xp.system.currentLevel);
    }
    private void LoadEXP()
    {
        xp.system.SetLevel(PlayerPrefs.GetInt("LevelXP", 0));
        xp.system.currentValue = PlayerPrefs.GetFloat("CurrentXP",100f);
        
    }
    private void SaveInventory() 
    {
        foreach (GameObject slot in library.slots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (inventorySlot.item == null)
                PlayerPrefs.SetString($"Inventory/{slot.transform.name}", "null");
            else
                PlayerPrefs.SetString($"Inventory/{slot.transform.name}", inventorySlot.item.itemName);
        }
    }
    private void LoadInventory()
    {
        foreach (GameObject slot in library.slots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            string itemName = PlayerPrefs.GetString($"Inventory/{slot.transform.name}","null");
            if (itemName == null) continue;
           

            foreach (Item item in allItems)
            {
                if (item.itemName != itemName) continue;
                library.UpdateItem(inventorySlot, item, slot.transform.GetChild(0));
            }
        }
    }
    private void SaveLocation()
    {
        PlayerPrefs.SetString("Location", SceneManager.GetActiveScene().name);
    }
    private string LoadLocation()
    {
        return PlayerPrefs.GetString("Location", "Vilage");
    }
    private void SaveHp()
    {
        PlayerPrefs.SetInt("LevelHp", hp.hp.currentLevel);
        PlayerPrefs.SetFloat("CurrentHp", hp.hp.currentValue);
    }
    private void LoadHp()
    {
        hp.hp.SetLevel(PlayerPrefs.GetInt("LevelHp", 0));
        xp.system.currentValue = PlayerPrefs.GetFloat("CurrentHp",100f);
    }

}
