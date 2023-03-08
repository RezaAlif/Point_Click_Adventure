using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class HitBox : MonoBehaviour
{
    public bool isPlayer;
    public PlayerAttack playerAttack;
    public CharacterAlbility characterAlbility;

    private void OnTriggerEnter(Collider other)
    {
        if(isPlayer && other.tag == "Enemy")
        {
            CharacterAlbility enemyAlbility = other.GetComponent<CharacterAlbility>();
            enemyAlbility.Health -= characterAlbility.Damage;

            if(enemyAlbility.Health <= 0)
            {
                if(other.GetComponent<EnemyState>() != null)
                {
                    other.GetComponent<EnemyState>().ChangeState(CharacterState.Dead);
                    PlayerState.Instance.TargetTag = null;
                    playerAttack.FindNearestEnemyPath();
                }
            }
        }

        if(!isPlayer && other.tag == "Player")
        {
            CharacterAlbility playerAlbility = other.GetComponent<CharacterAlbility>();
            playerAlbility.Health -= characterAlbility.Damage;

            if(playerAlbility.Health <= 0)
            {
                PlayerState.Instance.ChangeState(CharacterState.Dead);
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(HitBox))]
public class HitBoxEditor : Editor
{
    
    SerializedProperty AlbilityField;
    SerializedProperty playerAttackField;

    private void OnEnable()
    {
        playerAttackField = serializedObject.FindProperty("playerAttack");
        AlbilityField = serializedObject.FindProperty("characterAlbility");
    }

    public override void OnInspectorGUI()
    {
        var hitBox = (HitBox)target;

        EditorGUILayout.PropertyField(AlbilityField, new GUIContent("Character Albility"));
        hitBox.isPlayer = EditorGUILayout.Toggle("Is Player", hitBox.isPlayer);

        if(hitBox.isPlayer)
        {
            EditorGUILayout.PropertyField(playerAttackField, new GUIContent("Player Attack"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
