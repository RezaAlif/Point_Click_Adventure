using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConfiguration : MonoBehaviour
{
    PlayerClick playerClick;
    CharacterAnimationController animationController;
    public string TextFile;
    public AnimationClip idleAnimation, talkAnimation;
    public GameObject DialogueObject;

    private void Start()
    {
        playerClick = FindObjectOfType<PlayerClick>();
        animationController = GetComponent<CharacterAnimationController>();
    }

    public void SpawnDialogue()
    {
        animationController.PlayAnimation(talkAnimation);
        GameObject dialogueObject = Instantiate(DialogueObject);
        dialogueObject.GetComponent<Dialouge>().textFile = TextFile;
        dialogueObject.GetComponent<Dialouge>().npcConfig = this;
    }

    public void FinishDialogue()
    {
        animationController.PlayAnimation(idleAnimation);
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
