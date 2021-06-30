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
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }
    public Text Namefield;
    public GameObject Error;
    public string username;

    public Text HighScorePanel;

    [SerializeField]
    private GameObject startMenu;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return; 
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    private void HideMenu()
    {
        startMenu.SetActive(false); 
    }

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
            username = Namefield.text;
            HideMenu(); 
            SceneManager.LoadScene(1);
        }

    }
    public void ResetScore()
    {
        MainManager.Instance.ResetScore();
        MainManager.Instance.LoadHighScore(); 
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
