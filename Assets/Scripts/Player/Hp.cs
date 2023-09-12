using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

interface IDamageble
{
    public void TakeDamage(float damageValue);
    public void Heal(float healValue);
    public void ResetHp();
    public void Die();
}

public abstract class Hp : ValueSystem
{
    [Header("HP UI Components")]
    [SerializeField] private List<Image> containers;
    [SerializeField] private Sprite emptyContainer, halfContainer, fullContainer;

    [Header("Armor UI Components")]
    [SerializeField] private List<Image> armorContainers;
    [SerializeField] private Sprite emptyArmorContainer, halfArmorContainer, fullArmorContainer;

    [SerializeField] private List<Image> armorContainersPlus;
    [SerializeField] private GameObject armorGroupPlus;

    [HideInInspector] public float _maxArmorValue;
    protected float _currentArmorValue;

    protected float _containerArmorValue = 10f;
    protected float _fullArmorContainers;
    protected float _halfArmorContainers;
    protected float _emptyArmorContainers;

    protected Coroutine _armorCoroutine;

    protected void Start()
    {
        ResetArmor();
        ResetHp();
        UpdateHP();
    }
    virtual public void Die()
    {
        Destroy(gameObject);
    }

    virtual public void Heal(float healValue)
    {
        currentValue += healValue;
    }

    virtual public void ResetHp()
    {
        maxValue = containers.Count * _containerValue;
        currentValue = maxValue;
    }

    virtual public void ResetArmor()
    {
        _maxArmorValue = armorContainers.Count * _containerArmorValue;
        _currentArmorValue = _maxArmorValue;
    }

    public void MergeArmor()
    {
        armorGroupPlus.SetActive(true);
        armorContainers.AddRange(armorContainersPlus);
        ResetArmor();
        UpdateArmor();
    }

    public void UnmergeArmor()
    {
        armorGroupPlus.SetActive(false);

        List<Image> endImages = new List<Image>();
        foreach (Image image in armorContainers)
        {
            if (armorContainersPlus.Contains(image)) continue;
            endImages.Add(image);
        }
        armorContainers = endImages;

        _maxArmorValue = armorContainers.Count * _containerArmorValue;
        UpdateArmor();
    }

    virtual public void UpdateHP()
    {
        UpdateContainers();
        int empties = (int)_emptyContainers;
        int half = (int)_halfContainers;
        int fulls = (int)_fullContainers;

        for (int i = containers.Count - 1; i >= 0; i--)
        {
            if (empties > 0)
            {
                empties--;
                containers[i].sprite = emptyContainer;
            }
            else if (half > 0)
            {
                half--;
                containers[i].sprite = halfContainer;
            }
            else if (fulls > 0)
            {
                fulls--;
                containers[i].sprite = fullContainer;
            }
        }
    }

    virtual public void UpdateArmor()
    {
        UpdateArmorContainers();
        int empties = (int)_emptyArmorContainers;
        int half = (int)_halfArmorContainers;
        int fulls = (int)_fullArmorContainers;

        for (int i = armorContainers.Count - 1; i >= 0; i--)
        {
            if (empties > 0)
            {
                empties--;
                armorContainers[i].sprite = emptyArmorContainer;
            }
            else if (half > 0)
            {
                half--;
                armorContainers[i].sprite = halfArmorContainer;
            }
            else if (fulls > 0)
            {
                fulls--;
                armorContainers[i].sprite = fullArmorContainer;
            }
        }

        if (_currentArmorValue != _maxArmorValue) return;
        if (_armorCoroutine == null) return;
        StopCoroutine(_armorCoroutine);
        _armorCoroutine = null;
    }

    virtual protected IEnumerator ArmorRecovery()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _currentArmorValue = Mathf.Clamp(_currentArmorValue + _containerArmorValue / 2, 0, _maxArmorValue);
            UpdateArmor();
        }
    }

    protected void UpdateArmorContainers()
    {
        _fullArmorContainers = _currentArmorValue / _containerArmorValue - _currentArmorValue % _containerArmorValue / _containerArmorValue;
        _halfArmorContainers = (_currentArmorValue % _containerArmorValue) / (_containerArmorValue / 2);
        _emptyArmorContainers = (_maxArmorValue - _currentArmorValue - _currentArmorValue % _containerArmorValue) / _containerArmorValue;
    }
}
