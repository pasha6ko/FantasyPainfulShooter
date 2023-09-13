using System.Collections.Generic;
using UnityEngine;

public abstract class ValueSystem
{
    public readonly int currentLevel;

    protected float _maxSystemsValue;
    protected float _currentSystemsValue;

    private float levelMultiplier;
   
    virtual public float maxValue
    {
        get => _maxSystemsValue;
        set { 
            _maxSystemsValue = value > 100 ? value : 100;
        }
    }
    public float currentValue
    {
        get => _currentSystemsValue;
        set { _currentSystemsValue = Mathf.Clamp(value, 0, maxValue); }
    }

    public ValueSystem()
    {
        levelMultiplier = 1.5f;
        SetUpgradeLevel(0);
        currentValue = maxValue;
    }

    
    public void SetUpgradeLevel(int value)
    {
        maxValue = 100;
        maxValue = maxValue * Mathf.Pow(levelMultiplier, value);
        _currentSystemsValue = value;
    }

    

}
