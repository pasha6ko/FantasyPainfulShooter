using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    [Header("Currency Components")]
    [SerializeField] private TextMeshProUGUI moneysText;

    private int _money = 0;

    public int money
    {
        get => _money;
        set
        {
            if (value < 0) return;
            _money = value;
            UpdateUI();
        }
    }

    public bool PlusNotMinus(int amount)
    {
        if (_money - amount < 0) return false;
        return true;
    }

    public void AddMoney(int amount) => money += amount > 0 ? amount : 0;
    public void RemoveMoney(int amount)
    {
        _money -= amount > 0 ? amount : 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        moneysText.text = money.ToString();
    }
}
