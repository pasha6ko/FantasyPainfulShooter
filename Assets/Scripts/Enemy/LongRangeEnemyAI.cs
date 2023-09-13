using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeEnemyAI : EnemyAI
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform aimPoint, firePoint;
    [SerializeField] private float timeBetweenShots;
    protected override IEnumerator ActiveProcces()
    {
        state = Mode.Active;
        animator.SetLayerWeight(1, 1f);
        if (isMoving) agent.speed = runSpeed;
        agent.SetDestination(player.position);
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            agent.SetDestination(player.position);
            Attack();
        }
    }

    protected override void Attack()
    {
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_activeProcces == null)
            animator.SetLayerWeight(1, 0f);

        if (player != null && state == Mode.Active)
            aimPoint.position = player.position;
        
    }
}
