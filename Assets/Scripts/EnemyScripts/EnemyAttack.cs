using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyState EnemyState;

    // Start is called before the first frame update
    void Start()
    {
        EnemyState = GetComponent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, EnemyState.playerObject.position) > EnemyState.minDistanceWithPlayer + 1)
        {
            EnemyState.ChangeState(CharacterState.Move);
            this.enabled = false;
        }
    }
}
