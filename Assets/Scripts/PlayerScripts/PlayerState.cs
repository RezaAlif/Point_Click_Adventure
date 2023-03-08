using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    MissionManager MissionManager;

    [Header("Class Player")] // Class Player Component
    public static PlayerState Instance;
    public CharacterAnimationController animationController;
    PlayerMovement playerMovement;
    PlayerGather playerGather;
    PlayerAttack playerAttack;

    public GameObject TargetTag;

    public AnimationClip idleAnimation;
    public AnimationClip talkAnimation;
    public AnimationClip deadAnimation;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MissionManager = FindObjectOfType<MissionManager>();
        playerGather = GetComponent<PlayerGather>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                animationController.PlayAnimation(idleAnimation);
                break;
            case CharacterState.Move:
                playerMovement.enabled = true;
                break;
            case CharacterState.Attack:
                playerAttack.enabled = true;
                playerAttack.SetAttack();
                break;
            case CharacterState.Gather:
                playerGather.enabled = true;
                break;
            case CharacterState.Talk:
                animationController.PlayAnimation(talkAnimation);
                break;
            case CharacterState.Dead:
                animationController.PlayAnimation(deadAnimation);
                ClearClass();
                break;
        }
    }

    void ClearClass()
    {
        Destroy(playerMovement);
        Destroy(playerGather);
        Destroy(playerGather);
        Destroy(this);
    }

    public void OnTarget()
    {
        if (TargetTag.GetComponent<EnemyState>() != null)
        {
            ChangeState(CharacterState.Attack);
        }
        if (TargetTag.GetComponent<ResourceScript>() != null)
        {
            ChangeState(CharacterState.Gather);
        }
        if (TargetTag.GetComponent<NPCConfiguration>() != null)
        {
            ChangeState(CharacterState.Talk);
            TargetTag.GetComponent<NPCConfiguration>().SpawnDialogue();
        }
        if (TargetTag.GetComponent<NavigationScript>() != null)
        {
            MissionManager.CheckMission();
        }
    }
}
