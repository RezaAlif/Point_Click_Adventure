using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyChase : MonoBehaviour
{
    EnemyState EnemyState;
    NavMeshAgent Agent;

    Vector3 startPoint;

    public AnimationClip RunAnimation;
    public AnimationClip AttackAnimation;

    // Start is called before the first frame update
    void Start()
    {
        EnemyState = GetComponent<EnemyState>();
        Agent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
    }

    public void SelectTarget()
    {
        EnemyState.characterAnimationController.PlayAnimation(RunAnimation);
        Agent.enabled = true;
        Agent.SetDestination(EnemyState.playerObject.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, EnemyState.playerObject.position) < EnemyState.minDistanceWithPlayer)
        {
            Agent.enabled = false;
            EnemyState.characterAnimationController.PlayAnimation(AttackAnimation);
            EnemyState.ChangeState(CharacterState.Attack);
            this.enabled = false;
        }
        if (Vector3.Distance(transform.position, EnemyState.playerObject.position) > EnemyState.maxDistanceWithPlayer)
        {
            PlayerAway();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == EnemyState.playerObject)
        {
            Agent.enabled = false;
            EnemyState.minDistanceWithPlayer = Vector3.Distance(transform.position, EnemyState.playerObject.position);
            EnemyState.characterAnimationController.PlayAnimation(AttackAnimation);
            EnemyState.ChangeState(CharacterState.Attack);
            this.enabled = false;
        }
    }

    void ToIdle()
    {
        Agent.SetDestination(startPoint);

        if (Vector3.Distance(transform.position, startPoint) < 0.1f)
        {
            Agent.enabled = false;
            EnemyState.ChangeState(CharacterState.Idle);
            this.enabled = false;
        }
    }

    void PlayerAway()
    {
        if (EnemyState.enemyPatrol != null)
        {
            EnemyState.ChangeState(CharacterState.Patrol);
            this.enabled = false;
        }
        else
        {
            ToIdle();
        }
    }
}
