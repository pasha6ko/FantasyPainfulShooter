using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private DamageZone damageZone;
    private PlayerHp _hp;
    private void OnTriggerEnter(Collider other)
    {
        _hp = other.transform.GetComponent<PlayerHp>();
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.GetComponent<PlayerHp>() != _hp) return;
        _hp = null;
    }

    public void SendMessageToAttack()
    {
        damageZone.Attack();
    }
    public void Attack()
    {
        if (_hp == null) return;
        _hp.TakeDamage(damage);
    }
}
