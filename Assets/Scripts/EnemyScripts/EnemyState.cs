using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;
    EnemyChase enemyChase;
    EnemyAttack enemyAttack;
    EnemyIdle enemyIdle;
    EnemyDead enemyDead;
    PlayerClick playerClick;

    public CharacterAnimationController characterAnimationController;
    [HideInInspector]public Transform playerObject;
    public float minDistanceWithPlayer = 0.1f;
    public float maxDistanceWithPlayer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChase = GetComponent<EnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyDead = GetComponent<EnemyDead>();
        playerClick = FindObjectOfType<PlayerClick>();
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
            enemyIdle = GetComponent<EnemyIdle>();
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
            case CharacterState.Dead:
                enemyDead.enabled = true;
                Destroy(this);
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

    private void OnMouseEnter()
    {
        playerClick.TaggedObject = this.gameObject;
    }

    private void OnMouseExit()
    {
        playerClick.TaggedObject = null;
    }
}
