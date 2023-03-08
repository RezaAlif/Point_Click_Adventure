using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour
{
    GameManager GameManager;
    public GameObject TaggedObject;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TaggedObject != null)
                {
                    if (TaggedObject != PlayerState.Instance.TargetTag)
                    {
                        PlayerState.Instance.TargetTag = TaggedObject;
                        PlayerState.Instance.ChangeState(CharacterState.Move);
                    }

                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if(hit.collider.tag != "Ignore")
                        {
                            transform.position = hit.point;
                            PlayerState.Instance.ChangeState(CharacterState.Move);
                        }
                    }
                }
            }

            if (PlayerState.Instance.TargetTag != null)
            {
                transform.position = PlayerState.Instance.TargetTag.transform.position;
            }
        }
    }
}
