using UnityEngine;

public class ExplosiveEnemyAI : EnemyAI
{
    protected override void Attack()
    {
        base.Attack();
        Destroy(gameObject, 1.5f);
    }
}
