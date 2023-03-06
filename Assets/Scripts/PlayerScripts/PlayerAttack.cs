using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public AnimationClip attackClip;

    public void SetAttack()
    {
        PlayerState.Instance.animationController.PlayAnimation(attackClip);
    }

    public void FindNearestEnemyPath()
    {
        var Enemies = FindObjectsOfType<EnemyState>();
        var nearestDist = float.MaxValue;
        Transform NearObject = null;

        foreach (var PointList in Enemies)
        {
            if (Vector3.Distance(transform.position, PointList.transform.position) < nearestDist)
            {
                nearestDist = Vector3.Distance(transform.position, PointList.transform.position);
                NearObject = PointList.transform;
            }
        }

        if(nearestDist > 1)
        {
            PlayerState.Instance.ChangeState(CharacterState.Idle);
            this.enabled = false;
        }
        else
        {
            PlayerState.Instance.TargetTag = NearObject.gameObject;
            PlayerState.Instance.ChangeState(CharacterState.Move);
            this.enabled = false;
        }
    }
}
