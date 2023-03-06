using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    Text uiText;
    string textToWrite;
    int characterIndex;
    float timePerCharacter;
    float timer;
    public bool textFill;
    public string currentText;

    public void AddText(Text uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(textToWrite != null && !textFill)
        {
            currentText = textToWrite;
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);

                if(characterIndex >= textToWrite.Length)
                {
                    textToWrite = null;
                    textFill = true;
                }
            }
        }
    }
}
