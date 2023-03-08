using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraScripts : MonoBehaviour
{
    public bool hasBorder;
    public Transform PlayerPosition;
    public Vector3 CameraOffset;
    public Vector3 MinPosition;
    public Vector3 MaxPosition;

    // Update is called once per frame
    void Update()
    {
        if (hasBorder)
        {
            transform.position = new Vector3(Mathf.Clamp(PlayerPosition.position.x + CameraOffset.x, MinPosition.x, MaxPosition.x),
                Mathf.Clamp(PlayerPosition.position.y + CameraOffset.y, MinPosition.y, MaxPosition.y),
                Mathf.Clamp(PlayerPosition.position.z + CameraOffset.z, MinPosition.z, MaxPosition.z));
        }
        else
        {
            transform.position = PlayerPosition.position + CameraOffset;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraScripts))]
public class CameraEditor : Editor
{
    SerializedProperty PlayerPos;

    private void OnEnable()
    {
        PlayerPos = serializedObject.FindProperty("PlayerPosition");
    }

    public override void OnInspectorGUI()
    {
        var cameraScript = (CameraScripts)target;
        
        EditorGUILayout.PropertyField(PlayerPos, new GUIContent("Player Position"));
        cameraScript.CameraOffset = EditorGUILayout.Vector3Field("Player Position", cameraScript.CameraOffset);
        cameraScript.hasBorder = EditorGUILayout.Toggle("Has Border", cameraScript.hasBorder);

        if (cameraScript.hasBorder)
        {
            cameraScript.MinPosition = EditorGUILayout.Vector3Field("Min Position", cameraScript.MinPosition);
            cameraScript.MaxPosition = EditorGUILayout.Vector3Field("Max Position", cameraScript.MaxPosition);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
