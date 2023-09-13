using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPSystem : MonoBehaviour
{
    [Header("Exp Components")]
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI expText;

    private ValueSystem _system = new ValueSystem();
    private int exp = 0;

    private void Awake()
    {
        expBar.fillAmount = 0;
        _system.SetLevel(0);
        _system.currentValue = 0;
    }

    public void AddExp(int value)
    {
        exp = (int)(_system.currentValue - _system.maxValue + value);
        _system.currentValue += value;
        print($"value = {value}\ncurrent = {_system.currentValue}\nmax = {_system.maxValue}\nexp = {exp}");

        if (_system.currentValue == _system.maxValue) NewLevel();
        UpdateEXP();
    }

    private void UpdateEXP()
    {
        expBar.fillAmount = _system.currentValue / _system.maxValue;
    }

    private void NewLevel()
    {
        expBar.fillAmount = 0;
        
        _system.currentValue = 0;
        _system.SetLevel(_system.currentLevel + 1);
        AddExp(exp);

        expText.text = $"{_system.currentLevel}";
    }
}