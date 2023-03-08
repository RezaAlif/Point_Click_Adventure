using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConfiguration : MonoBehaviour
{
    PlayerClick playerClick;
    public CharacterAnimationController animationController;
    public Object TextFile;
    public Object TextFileMission;
    public AnimationClip idleAnimation, talkAnimation;
    public GameObject DialogueObject;

    private void Start()
    {
        playerClick = FindObjectOfType<PlayerClick>();
    }

    public void SpawnDialogue()
    {
        animationController.PlayAnimation(talkAnimation);
        GameObject dialogueObject = Instantiate(DialogueObject);
        dialogueObject.GetComponent<Dialouge>().npcConfig = this;

        if(TextFileMission != null)
        {
            dialogueObject.GetComponent<Dialouge>().textFile = TextFileMission.name;
            dialogueObject.GetComponent<Dialouge>().IdentityText.text = gameObject.name;
            dialogueObject.GetComponent<Dialouge>().isMission = true;
        }
        else
        {
            dialogueObject.GetComponent<Dialouge>().textFile = TextFile.name;
            dialogueObject.GetComponent<Dialouge>().IdentityText.text = gameObject.name;
        }
    }

    public void FinishDialogue()
    {
        animationController.PlayAnimation(idleAnimation);
        PlayerState.Instance.TargetTag = null;
        TextFileMission = null;
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
