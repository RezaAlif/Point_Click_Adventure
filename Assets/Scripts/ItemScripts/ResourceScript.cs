using System;
using UnityEngine;

[Serializable]
public class RewardList
{
    public ItemEnum Item;
    public int Value;
}

public class ResourceScript : MonoBehaviour
{
    PlayerClick playerClick;
    public bool isMission;
    public float TimeGather;
    public RewardList[] rewardLists;


    private void Start()
    {
        playerClick = FindObjectOfType<PlayerClick>();
    }

    private void OnMouseEnter()
    {
        playerClick.TaggedObject = this.gameObject;
    }

    private void OnMouseExit()
    {
        playerClick.TaggedObject = null;
    }
}
