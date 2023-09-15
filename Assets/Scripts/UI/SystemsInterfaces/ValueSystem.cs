using System.Collections.Generic;
using UnityEngine;

public class ValueSystem
{
    public int currentLevel { get; private set; }

    protected float _maxSystemsValue;
    protected float _currentSystemsValue;

    protected float levelMultiplier;
   
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

    public ValueSystem(float maxValue = 100, int level = 0)
    {
        this.maxValue = maxValue;
        levelMultiplier = 1.5f;
        SetLevel(level);
        currentValue = maxValue;
    }   

    
    virtual public void SetLevel(int value)
    {
        maxValue = 100;
        maxValue = maxValue * Mathf.Pow(levelMultiplier, value);
        currentLevel = value;
    }

    virtual public void MaximizeValue()
    {
        currentValue = maxValue;
    }
    

}
