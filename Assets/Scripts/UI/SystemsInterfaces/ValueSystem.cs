using UnityEngine;

public abstract class ValueSystem : MonoBehaviour
{
    protected float _maxSystemsValue;
    protected float _currentSystemsValue;

    protected float _containerValue = 10f;
    protected float _fullContainers;
    protected float _halfContainers;
    protected float _emptyContainers;

    public float maxValue
    {
        get => _maxSystemsValue;
        set { _maxSystemsValue = value > 100 ? value : 100; }
    }

    public float currentValue
    {
        get => _currentSystemsValue;
        set { _currentSystemsValue = Mathf.Clamp(value, 0, maxValue); }
    }

    private void Start()
    {
        UpdateContainers();
    }

    protected void UpdateContainers()
    {
        _fullContainers = currentValue / _containerValue - currentValue % _containerValue / _containerValue;
        _halfContainers = (currentValue % _containerValue) / (_containerValue / 2);
        _emptyContainers = (maxValue - currentValue - currentValue % _containerValue) / _containerValue;
    }
}
