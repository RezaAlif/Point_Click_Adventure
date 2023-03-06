using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetContainer : MonoBehaviour
{
    public static AssetContainer Instance;
    public int Wood, Stone;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Wood = PlayerPrefs.GetInt(ItemEnum.Wood.ToString());
        Stone = PlayerPrefs.GetInt(ItemEnum.Stone.ToString());
    }

    public void SetValue(ItemEnum item, int value)
    {
        switch (item)
        {
            case ItemEnum.Wood:
                Wood += value;
                PlayerPrefs.SetInt(item.ToString(), Wood);
                break;
            case ItemEnum.Stone: 
                Stone += value;
                PlayerPrefs.SetInt(item.ToString(), Stone);
                break;
        }
    }
}
