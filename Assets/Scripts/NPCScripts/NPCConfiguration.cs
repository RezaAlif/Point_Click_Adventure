using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConfiguration : MonoBehaviour
{
    PlayerClick playerClick;
    public CharacterAnimationController animationController;
    public string TextFile;
    public AnimationClip idleAnimation, talkAnimation;
    public GameObject DialogueObject;
    public Transform CanvasLocation;

    private void Start()
    {
        playerClick = FindObjectOfType<PlayerClick>();
    }

    public void SpawnDialogue()
    {
        animationController.PlayAnimation(talkAnimation);
        GameObject dialogueObject = Instantiate(DialogueObject);
        dialogueObject.transform.SetParent(CanvasLocation, false);
        dialogueObject.GetComponent<Dialouge>().textFile = TextFile;
        dialogueObject.GetComponent<Dialouge>().npcConfig = this;
    }

    public void FinishDialogue()
    {
        animationController.PlayAnimation(idleAnimation);
        PlayerState.Instance.TargetTag = null;
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
