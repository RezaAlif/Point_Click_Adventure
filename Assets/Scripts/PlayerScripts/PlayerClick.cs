using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour
{
    public GameObject TaggedObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(TaggedObject != null)
            {
                transform.position = TaggedObject.transform.position;
                PlayerState.Instance.TargetTag = TaggedObject;
                PlayerState.Instance.ChangeState(CharacterState.Move);

            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    transform.position = hit.point;
                    PlayerState.Instance.ChangeState(CharacterState.Move);
                }
            }
        }
    }
}
