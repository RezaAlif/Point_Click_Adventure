using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour
{
    EnemyState EnemyState;
    public AnimationClip IdleAnimation;

    // Start is called before the first frame update
    void Start()
    {
        EnemyState = GetComponent<EnemyState>();
    }

    private void OnEnable()
    {
        EnemyState.characterAnimationController.PlayAnimation(IdleAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, EnemyState.playerObject.position) < EnemyState.maxDistanceWithPlayer)
        {
            EnemyState.ChangeState(CharacterState.Move);
            this.enabled = false;
        }
    }
}
