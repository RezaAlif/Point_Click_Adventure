using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadar : MonoBehaviour
{
    GameManager GameManager;
    MissionManager MissionManager;
    public GameObject RadarSpriteObject;

    // Start is called before the first frame update
    void Start()
    {
        MissionManager = FindObjectOfType<MissionManager>();
        GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStart)
        {
            RadarSpriteObject.SetActive(true);
            var nearestDist = float.MaxValue;
            Transform NearObject = null;

            foreach (var PointList in MissionManager.MissionList[MissionManager.MissionUnit].ObjectActivated)
            {
                if (PointList != null)
                {
                    if (Vector3.Distance(transform.position, PointList.transform.position) < nearestDist)
                    {
                        nearestDist = Vector3.Distance(transform.position, PointList.transform.position);
                        NearObject = PointList.transform;
                    }
                }
            }

            Vector3 newTarget = new Vector3(NearObject.position.x, transform.position.y, NearObject.position.z);
            transform.LookAt(newTarget);
        }
        else
        {
            RadarSpriteObject.SetActive(false);
        }
    }
}
