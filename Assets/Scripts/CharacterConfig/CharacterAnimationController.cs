using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Animator Animator;
    public AnimationClip currentClip;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationClip animationClip)
    {
        if(animationClip != currentClip)
        {
            Animator.Play(animationClip.name);
            currentClip = animationClip;
        }
    }

    public void TriggerDestroy()
    {
        Destroy(transform.root.gameObject);
    }
}
