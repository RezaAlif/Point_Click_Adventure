using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Class Player")] // Class Player Component
    public static PlayerState Instance;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    PlayerGather playerGather;

    [HideInInspector]public GameObject TargetTag;

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
        playerGather = GetComponent<PlayerGather>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                playerAttack.enabled = false;
                playerMovement.enabled = false;
                playerGather.enabled = false;
                break;

            case CharacterState.Move:
                playerAttack.enabled = false;
                playerMovement.enabled = true;
                playerGather.enabled = false;

                playerMovement.SelectTarget();
                break;
            case CharacterState.Attack:
                playerAttack.enabled = true;
                playerGather.enabled = false;
                playerMovement.enabled = false;
                break;
            case CharacterState.Gather:
                playerGather.enabled = true;
                playerMovement.enabled = false;
                playerAttack.enabled = true;
                break;
        }
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
    }
}
