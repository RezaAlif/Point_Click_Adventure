using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    EnemyState enemyState;
    MissionManager MissionManager;
    public AnimationClip DeadAnimation;
    public RewardList[] rewardLists;
    public bool IsMission = false;

    // Start is called before the first frame update
    void Start()
    {
        MissionManager = FindObjectOfType<MissionManager>();
        this.gameObject.tag = "Untagged";
        enemyState = GetComponent<EnemyState>();
        enemyState.characterAnimationController.PlayAnimation(DeadAnimation);

        for (int i = 0; i < rewardLists.Length; i++)
        {
            AssetContainer.Instance.SetValue(rewardLists[i].Item, rewardLists[i].Value);
        }

        if (IsMission)
        {
            MissionManager.CheckMission();
        }
    }
}
