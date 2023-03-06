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
    public float SpeedMove = 4f;

    public AnimationClip RunAnimation;
    public AnimationClip AttackAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        EnemyState = GetComponent<EnemyState>();
        Agent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
    }

    public void SelectTarget()
    {
        EnemyState.characterAnimationController.PlayAnimation(RunAnimation);
        Agent.enabled = true;
        Agent.speed = SpeedMove;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(EnemyState.playerObject.position);

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
