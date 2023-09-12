using UnityEngine;

public class ExplosiveEnemyAI : EnemyAI
{
    [SerializeField] private float explosionTriggerDistance = 2f;
    /*
    private void OnTriggerStay(Collider other)
    {
        if (player == null) return;
        if (other.transform != player) return;
        if (Vector3.Distance(transform.position, player.position) > explosionTriggerDistance) return;
        animator.SetTrigger("Attack");
    }*/
}
