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
    //public MainManager mainManager;
    public int difficultyValue = 1;
    public TMP_InputField inputname;
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI HighScoreView;
    public string playerName;


    void Start()
    {
        MainManager.Instance.LoadPlayerSettings();
        //MainManager.Instance.CallEm();
        //highestScore.text = "Playar: " + MainManager.Instance.playerName + "| Score: " +MainManager.Instance.loadHighestScore.ToString();
        //HighScoreView.text.Insert(0, highestScore.text);
    }

    public void ButtonClicked(int diff)
    {
        // Set the value when the button is clicked
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
        /*string path = Application.persistentDataPath + "/players.json";
        if (File.Exists(path))
        {
            MainManager.Instance.LoadPlayerSettings(playerName);//, difficultyValue);
        }
        else
        {*/
            MainManager.Instance.SavePlayerSettings(playerName,0);//, difficultyValue, 0);
            
        //}
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
