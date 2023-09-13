using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private DamageZone damageZone;
    [SerializeField] private PlayerHp _hp;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_hp != null) return;
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
