using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;
    EnemyChase enemyChase;
    EnemyAttack enemyAttack;
    EnemyIdle enemyIdle;

    public CharacterAnimationController characterAnimationController;
    public Transform playerObject;
    public float minDistanceWithPlayer = 0.1f;
    public float maxDistanceWithPlayer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = FindObjectOfType<PlayerState>().transform;
        enemyChase = GetComponent<EnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyIdle = GetComponent<EnemyIdle>();
        PatrolCheck();
    }

    void PatrolCheck()
    {
        if (enemyPatrol != null)
        {
            ChangeState(CharacterState.Patrol);
        }
        else
        {
            ChangeState(CharacterState.Idle);
        }
    }

    public void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                enemyIdle.enabled = true;
                break;
            case CharacterState.Patrol:
                enemyPatrol.enabled = true;
                FindNearestPatrolPath();
                break;
            case CharacterState.Move:
                enemyChase.enabled = true;
                enemyChase.SelectTarget();
                break;
            case CharacterState.Attack:
                enemyAttack.enabled = true;
                break;
        }
    }



    void FindNearestPatrolPath()
    {
        var nearestDist = float.MaxValue;
        Vector3 NearObject = Vector3.zero;

        foreach (var PointList in enemyPatrol.listPath)
        {
            if (Vector3.Distance(transform.position, PointList) < nearestDist)
            {
                nearestDist = Vector3.Distance(transform.position, PointList);
                NearObject = PointList;
            }
        }

        enemyPatrol.SelectTarget(NearObject);
    }
}
