using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {forward, back, left, right};

public class VillagerRunaway : MonoBehaviour
{
    Animator Animator;
    public AnimationClip AnimationClip;
    public Direction Direction;
    public float Speed;
    public float MaxPath;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.Play(AnimationClip.name);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        switch (Direction)
        {
            case Direction.forward:
                if(transform.position.z > MaxPath)
                {
                    Destroy(gameObject);
                }
                break;
            case Direction.back:
                if (transform.position.z < MaxPath)
                {
                    Destroy(gameObject);
                }
                break;
            case Direction.left:
                if (transform.position.x < MaxPath)
                {
                    transform.Translate(transform.right * Speed * Time.deltaTime);
                    Destroy(gameObject);
                }
                break;
            case Direction.right:
                transform.Translate(transform.right * -Speed * Time.deltaTime);
                if (transform.position.x > MaxPath)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
