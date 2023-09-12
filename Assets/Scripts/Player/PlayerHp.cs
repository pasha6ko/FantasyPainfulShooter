using UnityEngine;



public class PlayerHp : Hp
{
    override public void Die()
    {
        Destroy(gameObject);
    }

    override public void Heal(float healValue)
    {
        currentValue += healValue;
        UpdateHP();
    }

    public void AddArmor(float armorValue)
    {
        _currentArmorValue += armorValue;
        _currentArmorValue = Mathf.Clamp(_currentArmorValue, 0, _maxArmorValue);
        UpdateArmor();
    }

    public void TakeDamage(float damageValue)
    {
        if (damageValue <= 0) return;
        if (damageValue > _currentArmorValue)
        {
            damageValue -= _currentArmorValue;
            _currentArmorValue = 0;
            currentValue -= damageValue;

            UpdateArmor();
            UpdateHP();
        }
        else
        {
            _currentArmorValue -= damageValue;
            UpdateArmor();
        }
        if (currentValue <= 0f) Die();
        if (_armorCoroutine != null) return;
        _armorCoroutine = StartCoroutine(ArmorRecovery());
    }
}
