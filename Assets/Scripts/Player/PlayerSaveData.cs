using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveData : MonoBehaviour
{
    public static PlayerSaveData Instance;
    [SerializeField] private bool save;
    [SerializeField] private PlayerHp hp;
    [SerializeField] private EXPSystem xp;
    [SerializeField] private ItemLibrary library;
    [SerializeField] private List<Item> allItems = new List<Item>();
    [SerializeField] private CurrencySystem currency;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        LoadData();
    }
    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }
    public void SaveData()
    {
        if (!save) return;
        SaveEXP();
        SaveHp();
        SaveInventory();
        SaveLocation();
        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        if (!save) return;
        LoadEXP();
        LoadHp();
        LoadInventory();
    }
    public string LoadLocation()
    {
        return PlayerPrefs.GetString("Location", "Vilage");
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
        PlayerPrefs.SetInt("Money", currency.money);
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
        currency.AddMoney(PlayerPrefs.GetInt("Money",300));
    }
    private void SaveLocation()
    {
        PlayerPrefs.SetString("Location", SceneManager.GetActiveScene().name);
    }
    
    private void SaveHp()
    {
        PlayerPrefs.SetInt("LevelHp", hp.hp.currentLevel);
        PlayerPrefs.SetFloat("CurrentHp", hp.hp.currentValue);
    }
    private void LoadHp()
    {
        hp.hp.SetLevel(PlayerPrefs.GetInt("LevelHp", 0));
        hp.hp.currentValue = PlayerPrefs.GetFloat("CurrentHp",100f);
    }

}
