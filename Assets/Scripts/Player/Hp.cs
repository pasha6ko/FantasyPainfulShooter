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

public abstract class Hp : MonoBehaviour , IDamageble
{
    [SerializeField] protected float startHp, hp;

    protected void Start()
    {
        ResetHp();
    }
    virtual public void Die()
    {
        Destroy(gameObject);
    }

    virtual public void Heal(float healValue)
    {
        hp += healValue;
        hp = Mathf.Clamp(hp, 0, startHp);
    }

    virtual public void ResetHp()
    {
        hp = startHp;
    }

    virtual public void TakeDamage(float damageValue)
    {
        hp -= damageValue;
        hp = Mathf.Clamp(hp, 0, startHp);
        if (hp <= 0f) Die();
    }
}
