using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Mission
{
    public int TargetValue;
    public string MissionDescription;
    public GameObject[] ObjectActivated;
}

public class MissionManager : MonoBehaviour
{
    GameManager GameManager;
    public Mission[] MissionList;
    public int MissionUnit;
    public int MissionValue;

    public GameObject TransitionObject;
    public UnityEngine.Object NextLevel;

    void Start()
    {
        GameManager = GetComponent<GameManager>();
    }

    public void CheckMission()
    {
        if (MissionValue < MissionList[MissionUnit].TargetValue - 1)
        {
            MissionValue++;
        }
        else
        {
            MissionCompleted();
        }
    }

    public void MissionCompleted()
    {
        if (MissionUnit >= MissionList.Length - 1)
        {
            GameObject TransitionObj = Instantiate(TransitionObject);
            TransitionObj.GetComponent<TransitionScript>().NextScene = NextLevel.name;
            GameManager.gameStart = false;
            MissionValue = 0;
        }
        else
        {
            MissionUnit++;
            MissionValue = 0;

            if(MissionList[MissionUnit].ObjectActivated.Length > 0)
            {
                for(int i = 0;i < MissionList[MissionUnit].ObjectActivated.Length;i++)
                {
                    MissionList[MissionUnit].ObjectActivated[i].SetActive(true);
                }
            }
        }
    }
}
