using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    public Text Namefield;
    public GameObject Error; 

    public void ShowErrorPromt()
    {
        Error.gameObject.SetActive(true);  
    }
    public void HideErrorPromt()
    {
        Error.gameObject.SetActive(false);
    }
    public void StartNew()
    {
        if (Namefield.text.Length == 0) ShowErrorPromt();
        else
        {
            if(MainManager.Instance != null) MainManager.Instance.username = Namefield.text; 
            SceneManager.LoadScene(1);
        }

    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif

    }
}
