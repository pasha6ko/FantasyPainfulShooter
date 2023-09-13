using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerValueSystem : ValueSystem
{
    public float containerValue { get; private set; }

    public override float maxValue
    {
        get => _maxSystemsValue;
        set
        {
            _maxSystemsValue = value > 100 ? value : 100;
            containerValue = _maxSystemsValue / 10f;
        }
    }

    public ContainerValueSystem() : base() 
    {
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
}
