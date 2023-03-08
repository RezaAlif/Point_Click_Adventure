using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public CharacterAlbility CharacterAlbility;
    public Image healthImage;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = CharacterAlbility.Health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        healthImage.fillAmount = CharacterAlbility.Health / maxHealth;
    }
}
