using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIConfig : MonoBehaviour
{
    GameManager GameManager;
    MissionManager MissionManager;
    public CharacterAlbility playerAlbility;

    public TextMeshProUGUI missionText;

    public Image playerHeathBar;
    float playerMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        MissionManager = FindObjectOfType<MissionManager>();
        playerMaxHealth = playerAlbility.Health;
    }

    // Update is called once per frame
    void Update()
    {
        playerHeathBar.fillAmount = playerAlbility.Health / playerMaxHealth;
        missionText.text = MissionManager.MissionList[MissionManager.MissionUnit].MissionDescription;
    }
}
