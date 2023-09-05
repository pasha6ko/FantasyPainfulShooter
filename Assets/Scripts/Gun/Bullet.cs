using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;
    private void Start()
    {
        Destroy(gameObject,10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageble hp = other.transform.GetComponent<IDamageble>();
        if(hp!=null) hp.TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
