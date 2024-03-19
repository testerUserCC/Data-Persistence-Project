using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;
using TMPro;
using System.Linq;


public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public TextMeshProUGUI[] scoreTextList = new TextMeshProUGUI[7];
    public string[] nameList = new string[7];
    public int[] scoreList = new int[7];

    public string playerName;
    public int playerScore;

    /*public int difficulty;
    public int initScore;
    public int highestScore;

    public int loadDifficulty;
    public int loadScore;
    public int loadHighestScore;*/

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void CallEm()
    {
        UpdateScore();
        StartCoroutine(WaitAndUpdate());
    }
    [System.Serializable]
    class SaveData
    {
        public string[] playerName = new string[7];
        public int[] playerScore = new int[7];

        //public string playerName;
        /*public int difficulty;
        public int initScore;
        public int highestScore;

        public int loadDifficulty;
        public int loadScore;
        public int loadHighestScore;*/
    }
    public void SavePlayerSettings(string pName, int score)//,int diffLvl,int score)
    {
        Debug.Log($"1.0 Saving playerName: {pName}, and his score: {score}");
            playerName = pName;
            playerScore = score;
        //difficulty = diffLvl;
        //initScore = score;
        Debug.Log($"1.1 Saving playerName: {pName} || {playerName}, and his score: {score} || {playerScore}");
        SaveData savingTemp = new()
            {
                playerName = nameList,
                //difficulty = diffLvl,
                playerScore = scoreList,

            };
        Debug.Log($"1.2 Saving playerName: {pName} || {playerName}, and his score: {score} || {playerScore}");
        Debug.Log($"1.3 Saving playerName: {nameList[0]}, and his score: {scoreList[0]}");
        string json = JsonUtility.ToJson(savingTemp);
            File.WriteAllText(Application.persistentDataPath + "/players.json", json);
        UpdateScore();
    }
    public void LoadPlayerSettings()//, int lDiffValue)
    {
        //playerName = lPName;
        //difficulty = lDiffValue;
        //initScore = 0;
        string path = Application.persistentDataPath + "/players.json";
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            SaveData loadDataTemp = JsonUtility.FromJson<SaveData>(json);

            nameList = loadDataTemp.playerName;
            scoreList = loadDataTemp.playerScore;
            //playerName = loadDataTemp.playerName;
            //loadDifficulty = loadDataTemp.difficulty;
            //loadScore = loadDataTemp.initScore;
        }
    }
    public void UpdateScore()
    {
        int index = scoreList.Length;

        Debug.Log($"2.1 index value is: {index} because scoreList.Length is: {scoreList.Length}");
        Debug.Log($"2.2 scoreList[index - 1] is: {scoreList[index - 1]}, and playerScore is: {playerScore}, and index is: {index}");
        while ((scoreList[index - 1] < playerScore) && index > -1)
        {
            Debug.Log($"2.3 scoreList[index - 1] is: {scoreList[index - 1]}, and playerScore is: {playerScore}, and index is: {index}");
            index-=1;
            Debug.Log($"2.4 scoreList[index - 1] is: {scoreList[index - 1]}, and playerScore is: {playerScore}, and index is: {index}");
        }

        if (index == scoreList.Length - 1)
        {
            scoreList[index] = playerScore;
            nameList[index] = playerName;
        }
        else if (index < scoreList.Length - 1)
        {
            for (int i = scoreList.Length - 1; i > index; i--)
            {
                scoreList[i] = scoreList[i - 1];
                nameList[i] = nameList[i - 1];
            }

            scoreList[index] = playerScore;
            nameList[index] = playerName;
        }
    }
    IEnumerator WaitAndUpdate()
    {
        yield return new WaitForSeconds(1);
        UpdateTextList();
    }
    public void UpdateTextList()
    {


        for (int i = 0; i < nameList.Length; i++)
        {
            scoreTextList[i] = GameObject.Find("Canvas").transform.Find("NameAndScore" + i).GetComponent<TextMeshProUGUI>();


            string name = nameList[i];
            int score = scoreList[i];

            int totalLength = 20;

            string formattedString = string.Format("{0,-10}{1,10}", name, score);
            scoreTextList[i].SetText(formattedString.PadRight(totalLength));
        }

    }



    /*public void SaveLeaderBoard(string[] lbPname, int lbScore)
    {
        playerName = lbPname[0];
        highestScore = Mathf.Max(lbScore, loadScore <= 0 ? 0 : loadScore);
        SaveData savingLB = new()
        {
            playerName = lbPname,
            highestScore = Mathf.Max(lbScore, highestScore)
        };
        string json = JsonUtility.ToJson(savingLB);
        File.AppendText(Application.persistentDataPath + "/LeaderBoard.json");
        //File.WriteAllText(Application.persistentDataPath + "/LeaderBoard.json", json);
    }
    public void LoadLeaderBoard()
    {
        string path = Application.persistentDataPath + "/LeaderBoard.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData loadDataLB = JsonUtility.FromJson<SaveData>(json);
            playerName = loadDataLB.playerName;
            loadHighestScore = loadDataLB.highestScore;
        }
    }*/
}
