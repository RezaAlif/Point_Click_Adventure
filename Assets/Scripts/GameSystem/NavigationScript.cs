using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationScript : MonoBehaviour
{
    PlayerClick playerClick;
    public bool isMission;
    //public Transform NextMove;

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
