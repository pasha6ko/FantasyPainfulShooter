using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageble
{
    public void TakeDamage(float damageValue);
    public void Heal(float healValue);
    public void ResetHp();
    public void Die();
}

public class PlayerHp : MonoBehaviour, IDamageble
{
    [SerializeField] private float startHp, hp;
    [SerializeField] public float maxArmor;
    [SerializeField] private float armor;

    private void Start()
    {
        ResetHp();
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(float healValue)
    {
        hp += healValue;
        hp = Mathf.Clamp(hp, 0, startHp);
    }

    public void AddArmor(float armorValue)
    {
        armor += armorValue;
        armor = Mathf.Clamp(armor, 0, maxArmor);
    }
    public void ResetHp()
    {
        hp = startHp;
    }

    public void TakeDamage(float damageValue)
    {
        if (damageValue > 0)
        {
            float armorDamage = armor;
            armor -= damageValue;
            if (armor <= 0) damageValue -= armorDamage;
            armor = Mathf.Clamp(armor,0, maxArmor);
        }
        hp -= damageValue;
        hp = Mathf.Clamp(hp, 0, startHp);
        if (hp <= 0f) Die();
    }
}
