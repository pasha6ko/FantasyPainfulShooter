using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public bool isMoving = true;
    public Mode state;
    
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float walkSpeed, runSpeed;
    [SerializeField] protected float fieldOfView, distanceToPlayer;
    [SerializeField] protected bool isActiveInSearch;
    [SerializeField] protected Animator animator;
    [SerializeField] protected AttackTrigger trigger;
    [Header("Sounds")]
    [SerializeField] protected GameObject detectionSound;
    [SerializeField] protected GameObject zombieSound;
    protected Action TargetFounded, TargetLosted;
    [SerializeField] protected Transform player;
    protected Coroutine _searching, _activeProcces;

    public enum Mode
    {
        Idle,
        Search,
        Active,
        Dead
    }
    protected void Start()
    {
        TargetFounded += FoundTrager;
        TargetLosted += LostTarget;
        if(trigger!=null)
            trigger.attackTriggerAction += Attack;
        agent.speed = walkSpeed;
        state = Mode.Idle;
    }
    protected virtual void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 0.1)
            Stop();
        animator.SetFloat("Speed", agent.speed);

        if (IsSeePlayer()) return;

        state = Mode.Idle;
        agent.speed = 0;

        if (isActiveInSearch)
            if (isMoving) agent.speed = walkSpeed;
        
    }
    protected void Stop()
    {
        agent.speed = 0f;
        agent.destination = transform.position;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        player = other.transform;
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (player == null) return;
        if (other.transform == player)
            player = null;  
    }

    virtual protected void Attack()
    {
        animator.SetTrigger("Attack");
    }
    virtual protected void FoundTrager()
    {
        if (_searching != null)
        {
            StopCoroutine(_searching);
            _searching = null;  
        }
        if (_activeProcces == null)
            _activeProcces = StartCoroutine(ActiveProcces());
    }

    virtual protected void LostTarget()
    {
        if (_activeProcces != null)
        {
            StopCoroutine(_activeProcces);
            _activeProcces = null;
        }
        if (_searching == null)
            _searching = StartCoroutine(SearchProcces());
    }
    
    protected bool IsSeePlayer()
    {
        if (player == null)
        {
            TargetLosted.Invoke();  
            return false;
        }
        if (player != null && state == Mode.Active) return true;

        Vector3 direction = player.position - transform.position;
        Vector3 angle = Quaternion.FromToRotation(transform.forward, direction).eulerAngles;

        if ((angle.x > 360 - 20 || angle.x < 0 + 20) && (angle.y > 360 - fieldOfView / 2 || angle.y < fieldOfView / 2))
        {
            TargetFounded.Invoke();
            return true;
        }
        return false;
    }
    protected IEnumerator SearchProcces()
    {
        state = Mode.Search;
        while (true)
        {
            if (isActiveInSearch) break;

            agent.SetDestination(transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
            yield return new WaitForSeconds(Random.Range(6f, 20f));
        }
    }
    virtual protected IEnumerator ActiveProcces()
    {
        state = Mode.Active;
        while (state == Mode.Active)
        {
            if (isMoving) agent.speed = runSpeed;
            agent.SetDestination(player.position);
            yield return null;
        }
        _activeProcces = null;
    }
    
}
