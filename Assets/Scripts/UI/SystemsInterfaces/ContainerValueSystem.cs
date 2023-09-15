using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerValueSystem : ValueSystem
{
    public float containerValue { get; set; }

    public override float maxValue
    {
        get => _maxSystemsValue;
        set
        {
            _maxSystemsValue = value > 100 ? value : 100;
            containerValue = _maxSystemsValue / 10f;
        }
    }

    public ContainerValueSystem(float maxValue = 100, int level = 0) : base(maxValue,level) 
    {
        this.maxValue = maxValue;
        levelMultiplier = 1.5f;
        SetLevel(level);
        currentValue = maxValue;
        containerValue = 10f;
    }

    public Dictionary<string, int> GetContainersInfo()
    {
        int _fullContainers = (int)(currentValue / containerValue - currentValue % containerValue / containerValue);
        int _halfContainers = (currentValue % containerValue) > 0 ? 1 : 0;
        return new Dictionary<string, int>()
        {   { "full", _fullContainers},
            { "half", _halfContainers}
        };
    }

    public override void SetLevel(int value)
    {
        base.SetLevel(value);

        containerValue = containerValue * Mathf.Pow(levelMultiplier, containerValue);
    }
}
