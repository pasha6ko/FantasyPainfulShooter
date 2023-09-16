using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : Hp
{
    [Header("HP UI Components")]
    [SerializeField] private List<Image> hpContainers;
    [SerializeField] private Sprite hpEmptyContainer, hpHalfContainer, hpFullContainer;

    [Header("Armor UI Components")]
    [SerializeField] private List<Image> armorContainers;
    [SerializeField] private Sprite emptyArmorContainer, halfArmorContainer, fullArmorContainer;

    public ContainerValueSystem armor = new ContainerValueSystem();

    private new void Start()
    {
        ResetHp();
        ResetArmor();
        UpdateHp();
    }
    override public void Die()
    {
        Destroy(gameObject);
    }

    override public void Heal(float healValue)
    {
        hp.currentValue += healValue;
        UpdateHp();
        UpdateArmor();
    }

    override public void ResetHp()
    {
        hp.currentValue = hp.maxValue;
        UpdateHp();
        UpdateArmor();
    }

    public override void TakeDamage(float damageValue)
    {
        if (damageValue <= 0) return;
        if (damageValue > armor.currentValue)
        {
            damageValue -= armor.currentValue;
            armor.currentValue = 0;
            hp.currentValue -= damageValue;
        }
        else
        {
            armor.currentValue -= damageValue;
        }
        UpdateHp();
        UpdateArmor();

        if (_armorCoroutine != null) return;
        _armorCoroutine = StartCoroutine(ArmorRecovery());
    }

    virtual public void ResetArmor()
    {
        armor.currentValue = armor.maxValue;
    }

    public void UpgradeArmor()
    {
        armor.SetLevel(armor.currentLevel + 1);
        ResetArmor();
        UpdateArmor();
    }
    public void UpgradeHp()
    {
        hp.SetLevel(hp.currentLevel + 1);
        ResetHp();
        UpdateHp();
    }

    virtual public void UpdateHp()
    {
        Dictionary<string, int> containersInfo = hp.GetContainersInfo();
        int half = containersInfo["half"];
        int fulls = containersInfo["full"];

        for (int i = 0; i < hpContainers.Count; i++)
        {
            if (fulls > 0)
            {
                fulls--;
                hpContainers[i].sprite = hpFullContainer;
            }
            else if (half > 0)
            {
                half--;
                hpContainers[i].sprite = hpHalfContainer;
            }
            else
            {
                hpContainers[i].sprite = hpEmptyContainer;
            }

        }
    }

    virtual public void UpdateArmor()
    {
        Dictionary<string, int> containersInfo = armor.GetContainersInfo();
        int half = containersInfo["half"];
        int fulls = containersInfo["full"];

        for (int i = armorContainers.Count - 1; i >= 0; i--)
        {
            if (fulls > 0)
            {
                fulls--;
                armorContainers[i].sprite = fullArmorContainer;
            }
            else if (half > 0)
            {
                half--;
                armorContainers[i].sprite = halfArmorContainer;
            }
            else
            {
                armorContainers[i].sprite = emptyArmorContainer;
            }
        }
        if (armor.currentValue == armor.maxValue) return;
        if (_armorCoroutine != null) return;
        _armorCoroutine = null;
    }

    virtual protected IEnumerator ArmorRecovery()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            armor.currentValue = Mathf.Clamp(armor.currentValue + armor.containerValue / 2, 0, armor.maxValue);
            UpdateArmor();
        }
    }

}
