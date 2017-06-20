using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayStoryGameHandler()
    {
        SceneManager.LoadScene("Entitas");
    }
    
    public void PlayRandomGameHandler()
    {
        SceneManager.LoadScene("RandomMap");
    }

    public void QuitHandler()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
         #endif
    }
}