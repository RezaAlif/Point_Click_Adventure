using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Dialouge : MonoBehaviour
{
    GameManager GameManager;
    MissionManager MissionManager;
    GameObject[] unitTalk;
    public NPCConfiguration npcConfig;
    public GameObject textObj;
    public GameObject uiContinue;
    public GameObject objActivated;
    public GameObject objDeactivated;
    public Transform textArea;
    int Unit;
    int textUnit;
    TextWriter m_Writer;
    public TextMeshProUGUI IdentityText;
    public string textFile;
    public bool gameStart;
    public bool isMission;
    List<string> fileLines;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        MissionManager = FindObjectOfType<MissionManager>();
        GameManager.gameStart = false;

        m_Writer = GetComponent<TextWriter>();
        string readFromFilePath = Path.Combine(Application.streamingAssetsPath, "TextFile", textFile + ".txt");
        StartCoroutine(GetAsset(readFromFilePath));
    }

    void ShowText()
    {
        for (int i = 0; i < fileLines.Count; i++)
        {
            GameObject objText = Instantiate(textObj, textArea);
            objText.transform.SetParent(textArea);
            unitTalk[i] = objText;
            objText.GetComponent<TMPro.TextMeshProUGUI>().text = fileLines[i];
            textUnit++;

            if (i == 0)
            {
                objText.SetActive(true);
            }
        }
        m_Writer.AddText(unitTalk[Unit].GetComponent<TMPro.TextMeshProUGUI>(), unitTalk[Unit].GetComponent<TMPro.TextMeshProUGUI>().text, 0.1f);
        unitTalk[Unit].GetComponent<TextMeshProUGUI>().text = "";
        m_Writer.textFill = false;
    }

    IEnumerator GetAsset(string uri)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        yield return webRequest.SendWebRequest();
        var content = webRequest.downloadHandler.text;
        var AllWords = content.Split('\n');
        fileLines = new List<string>(AllWords);
        unitTalk = new GameObject[fileLines.Count];
        ShowText();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return)))
        {
            if(Unit < textUnit - 1)
            {
                if (m_Writer.textFill)
                {
                    Unit++;
                    unitTalk[Unit].SetActive(true);
                    unitTalk[Unit - 1].SetActive(false);
                    m_Writer.AddText(unitTalk[Unit].GetComponent<TMPro.TextMeshProUGUI>(), unitTalk[Unit].GetComponent<TMPro.TextMeshProUGUI>().text, 0.1f);
                    unitTalk[Unit].GetComponent<TextMeshProUGUI>().text = "";
                    m_Writer.textFill = false;
                }
                else
                {
                    m_Writer.textFill = true;
                    unitTalk[Unit].GetComponent<TextMeshProUGUI>().text = m_Writer.currentText;
                }
            }
            else
            {
                if (m_Writer.textFill)
                {
                    Destroy(this.gameObject);
                    uiContinue.SetActive(false);

                    if(npcConfig != null)
                    {
                        npcConfig.FinishDialogue();
                    }
                    if(gameStart)
                    {
                        GameManager.gameStart = true;
                    }
                    if(objActivated != null)
                    {
                        objActivated.SetActive(true);
                    }
                    if (objDeactivated != null)
                    {
                        objDeactivated.SetActive(false);
                    }
                    if(isMission)
                    {
                        MissionManager.CheckMission();
                    }
                }
                else
                {
                    m_Writer.textFill = true;
                    unitTalk[Unit].GetComponent<TextMeshProUGUI>().text = m_Writer.currentText;
                }
            }
        }
    }
}
