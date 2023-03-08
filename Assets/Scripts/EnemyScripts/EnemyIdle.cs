using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour
{
    GameManager GameManager;
    EnemyState EnemyState;
    public AnimationClip IdleAnimation;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        EnemyState = GetComponent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStart)
        {
            EnemyState.characterAnimationController.PlayAnimation(IdleAnimation);

            if (Vector3.Distance(transform.position, EnemyState.playerObject.position) < EnemyState.maxDistanceWithPlayer)
            {
                EnemyState.ChangeState(CharacterState.Move);
                this.enabled = false;
            }
        }
    }
}
