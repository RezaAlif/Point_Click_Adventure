using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    GameManager GameManager;
    EnemyState EnemyState;
    NavMeshAgent Agent;
    public Vector3[] listPath;
    int UnitPath;
    public float SpeedMove = 2f;
    public AnimationClip PatrolAnimation;

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        EnemyState = GetComponent<EnemyState>();
        Agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Agent.enabled = true;
        Agent.speed = SpeedMove;
    }

    public void SelectTarget(Vector3 Target)
    {
        for (int i = 0; i < listPath.Length; i++)
        {
            if (listPath[i] == Target)
            {
                Agent.SetDestination(listPath[UnitPath]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStart)
        {
            Patroling();
            Chasing();
        }
    }

    void Patroling()
    {
        EnemyState.characterAnimationController.PlayAnimation(PatrolAnimation);

        if (Vector3.Distance(transform.position, listPath[UnitPath]) < 0.1f)
        {
            if (UnitPath >= listPath.Length - 1)
            {
                UnitPath = 0;
                Agent.SetDestination(listPath[UnitPath]);
            }
            else
            {
                UnitPath++;
                Agent.SetDestination(listPath[UnitPath]);
            }
        }
    }

    void Chasing()
    {
        if (Vector3.Distance(transform.position, EnemyState.playerObject.position) < EnemyState.maxDistanceWithPlayer)
        {
            EnemyState.ChangeState(CharacterState.Move);
            this.enabled = false;
        }
    }
}
