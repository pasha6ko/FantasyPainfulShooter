using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IDamageble
{
    public void TakeDamage(float damageValue);
    public void Heal(float healValue);
    public void ResetHp();
    public void Die();
}

public abstract class Hp : MonoBehaviour , IDamageble
{
    public ContainerValueSystem hp = new ContainerValueSystem();

    protected Coroutine _armorCoroutine;

    public float HP
    {
        get => hp.maxValue;
        set { hp.maxValue = value; }
    }

    public float hpMultiplier { get => hp.levelMultiplier; }

    virtual protected void Start()
    {
        ResetHp();
    }
    virtual public void Die()
    {
        Destroy(gameObject);
    }

    virtual public void Heal(float healValue)
    {
        hp.currentValue += healValue;
    }

    virtual public void ResetHp()
    {
        hp.currentValue = hp.maxValue;
    }
    virtual public void TakeDamage(float value)
    {
        hp.currentValue -= value;
        print(hp.currentValue);
        if (hp.currentValue <= 0)
            Die();
    }

}
