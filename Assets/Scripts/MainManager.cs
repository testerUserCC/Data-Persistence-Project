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
        //StartCoroutine(WaitAndUpdate());
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
        Debug.Log($"1.0 Saving playerName: {pName}, and his score: {score}");
            playerName = pName;
            playerScore = score;
        Debug.Log($"1.1 Saving playerName: {pName} || {playerName}, and his score: {score} || {playerScore}");
        
        
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
        Debug.Log($"2.1 index value is: {AIndex} because scoreList.Length is: {scoreList.Length}");
        while ((scoreList[AIndex] > playerScore) && AIndex < scoreList.Length)
        {
            Debug.Log($"2.3 scoreList[index - 1] is: {scoreList[AIndex]}, and playerScore is: {playerScore}, and index is: {AIndex}");
            AIndex++;
            Debug.Log($"2.4 scoreList[index - 1] is: {scoreList[AIndex]}, and playerScore is: {playerScore}, and index is: {AIndex}");
        }
        Debug.Log($"if (AIndex == scoreList.Length - 1) is if ({AIndex} == {scoreList.Length - 1})");
        if (AIndex == scoreList.Length - 1)
        {
            Debug.Log($"scoreList[AIndex] = playerScore; is {scoreList[AIndex]} = {playerScore}");
            scoreList[AIndex] = playerScore;
            Debug.Log($"nameList[AIndex] = playerName; is {nameList[AIndex]} = {playerName}");
            nameList[AIndex] = playerName;
        }

        else if (AIndex < scoreList.Length - 1)
        {
            Debug.Log($"else if (AIndex < scoreList.Length - 1) is else if ({AIndex} < {scoreList.Length - 1})");
            for (int i = scoreList.Length - 1; i > AIndex; i--)
            {
                scoreList[i] = scoreList[i - 1];
                nameList[i] = nameList[i - 1];
            }
            Debug.Log($"scoreList[AIndex] = playerScore; is {scoreList[AIndex]} = {playerScore}");
            scoreList[AIndex] = playerScore;
            Debug.Log($"nameList[AIndex] = playerName; is {nameList[AIndex]} = {playerName}");
            nameList[AIndex] = playerName;
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
}
