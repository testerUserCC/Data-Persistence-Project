using System.Collections;
using UnityEngine;
using System.IO;
using TMPro;



public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public TextMeshProUGUI[] scoreTextList = new TextMeshProUGUI[7];
    public string[] nameList = new string[7];
    public int[] scoreList = new int[7];

    public string playerName;
    public int playerScore;


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
        SavingToFile();
    }
    [System.Serializable]
    class SaveData
    {
        public string[] playerName = new string[7];
        public int[] playerScore = new int[7];


    }
    public void SavingToFile() 
    {
        SaveData savingTemp = new()
        {
            playerName = nameList,
            playerScore = scoreList,

        };

        string json = JsonUtility.ToJson(savingTemp);
        File.WriteAllText(Application.persistentDataPath + "/players.json", json);
    }
    public void SavePlayerSettings(string pName, int score)
    {
            playerName = pName;
            playerScore = score;
        
        
    }
    public void LoadPlayerSettings()
    {

        string path = Application.persistentDataPath + "/players.json";
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            SaveData loadDataTemp = JsonUtility.FromJson<SaveData>(json);

            nameList = loadDataTemp.playerName;
            scoreList = loadDataTemp.playerScore;

        }
    }
    public void UpdateScore()
    {
        int AIndex = 0;
        while ((scoreList[AIndex] > playerScore) && AIndex < scoreList.Length)
        {
            AIndex++;
        }
        if (AIndex == scoreList.Length - 1)
        {
            scoreList[AIndex] = playerScore;
            nameList[AIndex] = playerName;
        }

        else if (AIndex < scoreList.Length - 1)
        {
            for (int i = scoreList.Length - 1; i > AIndex; i--)
            {
                scoreList[i] = scoreList[i - 1];
                nameList[i] = nameList[i - 1];
            }
            scoreList[AIndex] = playerScore;
            nameList[AIndex] = playerName;
        }
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
}
