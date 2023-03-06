using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    EnemyState EnemyState;
    NavMeshAgent Agent;
    public Vector3[] listPath;
    public int UnitPath;
    public float distanceDetector = 10f;
    public AnimationClip PatrolAnimation;

    // Start is called before the first frame update
    void Start()
    {
        EnemyState = GetComponent<EnemyState>();
        Agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        EnemyState.characterAnimationController.PlayAnimation(PatrolAnimation);
        Agent.enabled = true;
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
        Patroling();
        Chasing();
    }

    void Patroling()
    {
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
