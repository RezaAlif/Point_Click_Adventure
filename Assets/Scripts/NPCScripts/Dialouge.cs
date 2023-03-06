using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Dialouge : MonoBehaviour
{
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
    public string nextScene;
    public string textFile;
    public bool gameStart;
    List<string> fileLines;

    private void Awake()
    {
        m_Writer = GetComponent<TextWriter>();
        string readFromFilePath = Path.Combine(Application.streamingAssetsPath, "TextFile", textFile + ".txt");
        StartCoroutine(GetAsset(readFromFilePath));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ShowText()
    {
        for (int i = 0; i < fileLines.Count; i++)
        {
            GameObject objText = Instantiate(textObj, textArea);
            objText.transform.SetParent(textArea);
            unitTalk[i] = objText;
            objText.GetComponent<Text>().text = fileLines[i];
            textUnit++;

            if (i == 0)
            {
                objText.SetActive(true);
            }
        }
        m_Writer.AddText(unitTalk[Unit].GetComponent<Text>(), unitTalk[Unit].GetComponent<Text>().text, 0.1f);
        unitTalk[Unit].GetComponent<Text>().text = "";
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
                    m_Writer.AddText(unitTalk[Unit].GetComponent<Text>(), unitTalk[Unit].GetComponent<Text>().text, 0.1f);
                    unitTalk[Unit].GetComponent<Text>().text = "";
                    m_Writer.textFill = false;
                }
                else
                {
                    m_Writer.textFill = true;
                    unitTalk[Unit].GetComponent<Text>().text = m_Writer.currentText;
                }
            }
            else
            {
                if (m_Writer.textFill)
                {
                    Destroy(this.gameObject);
                    uiContinue.SetActive(false);
                    if(gameStart)
                    {
                        //GameManager.instance.gameStart = true;
                    }
                    if(objActivated != null)
                    {
                        objActivated.SetActive(true);
                    }
                    if (objDeactivated != null)
                    {
                        objDeactivated.SetActive(false);
                    }
                    if(nextScene != "")
                    {
                        SceneManager.LoadScene(nextScene);
                    }
                }
                else
                {
                    m_Writer.textFill = true;
                    unitTalk[Unit].GetComponent<Text>().text = m_Writer.currentText;
                }
            }
        }
    }
}
