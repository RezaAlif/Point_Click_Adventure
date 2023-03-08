using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    Animator Animator;
    public string NextScene;
    public AnimationClip ShowLevel;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        DontDestroyOnLoad(this);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadAsyncScene());
    }

    public void ShowScene()
    {
        Animator.Play(ShowLevel.name);
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if(asyncLoad.isDone)
        {
            ShowScene();
        }
    }
}
