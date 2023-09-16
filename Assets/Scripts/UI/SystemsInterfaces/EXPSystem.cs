using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPSystem : MonoBehaviour
{
    [HideInInspector] public ValueSystem system = new ValueSystem(150, 0, 1.3f);

    [Header("Shop Components")]
    [SerializeField] public Abilities abilities;

    [Header("Exp Components")]
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI expText;

    private int exp = 0;

    private void Awake()
    {
        expBar.fillAmount = 0;
        system.SetLevel(0);
        system.currentValue = 0;
    }

    public void AddExp(int value)
    {
        exp = (int)(system.currentValue - system.maxValue + value);
        system.currentValue += value;

        if (system.currentValue == system.maxValue) NewLevel();
        UpdateEXP();
    }

    private void UpdateEXP()
    {
        expBar.fillAmount = system.currentValue / system.maxValue;
    }

    private void NewLevel()
    {
        expBar.fillAmount = 0;
        
        system.currentValue = 0;
        system.SetLevel(system.currentLevel + 1);
        AddExp(exp);

        expText.text = $"{system.currentLevel}";

        CheckAbilities();
    }

    public void CheckAbilities()
    {
        foreach (GameObject closure in abilities.closuresList)
        {
            if (abilities.abilitiesCount[closure] >= system.currentLevel)
            {
                closure.SetActive(false);
                continue;
            }
            closure.SetActive(true);
        }
    }
}