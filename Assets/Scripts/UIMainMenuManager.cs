using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class UIMainMenuManager : MonoBehaviour
{
    public int difficultyValue = 1;
    public TMP_InputField inputname;
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI HighScoreView;
    public string playerName;


    void Start()
    {
        MainManager.Instance.LoadPlayerSettings();
    }

    public void ButtonClicked(int diff)
    {
        if (diff == 1)
        { 
            difficultyValue = 1;
    }
        else if (diff == 2) {
            difficultyValue = 2;
        }
        else if (diff == 3) {
            difficultyValue = 3;
        }
    }

    public void StartNew()
    {
        playerName = inputname.text;
        MainManager.Instance.SavePlayerSettings(playerName,0);
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
