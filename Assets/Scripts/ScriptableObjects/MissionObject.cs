using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "MissionObject", menuName = "ScriptableObjects/MissionObject")]
public class MissionObject : ScriptableObject
{
    public bool SingleMission;
    public int TargetValue;
    public string MissionDescription;
}


