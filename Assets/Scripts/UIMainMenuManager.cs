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
    public int difficultyValue = 2;
    public TMP_InputField inputname;
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI difficultyText;
    public string playerName;


    void Start()
    {
        MainManager.Instance.LoadPlayerSettings();
        highestScore.text = MainManager.Instance.nameList[0];
    }

    public void ButtonClicked(int diff)
    {
        if (diff == 1)
        { 
            difficultyValue = 1;
            difficultyText.color = Color.green;
    }
        else if (diff == 2) {
            difficultyValue = 2;
            difficultyText.color = Color.blue;
        }
        else if (diff == 3) {
            difficultyValue = 3;
            difficultyText.color = Color.red;
        }
    }

    public void StartNew()
    {
        playerName = inputname.text;
        MainManager.Instance.SavePlayerSettings(playerName,0);
        MainManager.Instance.SetDifficultyValue(difficultyValue);
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
