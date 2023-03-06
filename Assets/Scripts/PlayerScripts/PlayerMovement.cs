using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent Agent;
    public Transform PointPosition;
    public Transform CameraPosition;
    public Vector3 CameraOffset;
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
        Agent.SetDestination(PointPosition.position);
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition.position = transform.position + CameraOffset;

        if (Vector3.Distance(transform.position, PointPosition.position) < 0.1f)
        {
            if(PlayerState.Instance.TargetTag != null)
            {
                PlayerState.Instance.OnTarget();
                this.enabled = false;
            }
            else
            {
                PlayerState.Instance.ChangeState(CharacterState.Idle);
                this.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerState.Instance.TargetTag)
        {
            PlayerState.Instance.OnTarget();
            this.enabled = false;
        }
    }
}
