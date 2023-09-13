using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ContainerValueSystem : ValueSystem
{
    private float _containerValue = 10f;

    public override float maxValue
    {
        get => _maxSystemsValue;
        set
        {
            _maxSystemsValue = value > 100 ? value : 100;
            _containerValue = _maxSystemsValue / 10f;
        }
    }

    public ContainerValueSystem() : base() 
    {
        
    }

    protected Dictionary<string, int> UpdateContainers()
    {

        int _fullContainers = (int)(currentValue / _containerValue - currentValue % _containerValue / _containerValue);
        int _halfContainers = (currentValue % _containerValue) > 0 ? 1 : 0;
        return new Dictionary<string, int>()
        {   { "full", _fullContainers},
            { "half", _halfContainers}
        };
    }
}
