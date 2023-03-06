using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGather : MonoBehaviour
{
    ResourceScript resourcesScript;
    public AnimationClip GatherAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        resourcesScript = PlayerState.Instance.TargetTag.GetComponent<ResourceScript>();
        PlayerState.Instance.animationController.PlayAnimation(GatherAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        resourcesScript.TimeGather -= Time.deltaTime;

        if(resourcesScript.TimeGather <= 0)
        {
            GetReward();
            Destroy(PlayerState.Instance.TargetTag);
            PlayerState.Instance.TargetTag = null;
            PlayerState.Instance.ChangeState(CharacterState.Idle);
            this.enabled = false;
        }
    }

    void GetReward()
    {
        for (int i = 0; i < resourcesScript.rewardLists.Length; i++)
        {
            AssetContainer.Instance.SetValue(resourcesScript.rewardLists[i].Item, resourcesScript.rewardLists[i].Value);
        }
    }
}
