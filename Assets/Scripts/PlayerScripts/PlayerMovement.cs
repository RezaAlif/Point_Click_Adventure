using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent Agent;
    public Transform PointPosition;
    public AnimationClip RunAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        Agent.enabled = false;
    }

    private void OnEnable()
    {
        Agent.enabled = true;
        PlayerState.Instance.animationController.PlayAnimation(RunAnimation);
    }

    public void SelectTarget()
    {
        if(PlayerState.Instance.TargetTag != null)
        {
            Agent.SetDestination(PlayerState.Instance.TargetTag.transform.position);
        }
        else
        {
            Agent.SetDestination(PointPosition.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SelectTarget();

        if (PlayerState.Instance.TargetTag != null)
        {
            if (Vector3.Distance(transform.position, PointPosition.position) < 2f)
            {
                if (PlayerState.Instance.TargetTag != null)
                {
                    PlayerState.Instance.OnTarget();
                    this.enabled = false;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, PointPosition.position) < 0.1f)
            {
                PlayerState.Instance.ChangeState(CharacterState.Idle);
                this.enabled = false;
            }
        }
    }
}
