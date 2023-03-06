using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    EnemyState enemyState;
    public AnimationClip DeadAnimation;
    public RewardList[] rewardLists;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = GetComponent<EnemyState>();
        enemyState.characterAnimationController.PlayAnimation(DeadAnimation);

        for (int i = 0; i < rewardLists.Length; i++)
        {
            AssetContainer.Instance.SetValue(rewardLists[i].Item, rewardLists[i].Value);
        }
    }
}
