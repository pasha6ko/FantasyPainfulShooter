using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage, speed;

    private PlayerHp hp;

    private void Start()
    {
        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        hp = other.GetComponent<PlayerHp>();
        if (hp != null)
            hp.TakeDamage(damage);

        Destroy(gameObject);
    }
}
