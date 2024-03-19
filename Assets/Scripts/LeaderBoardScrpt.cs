using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LeaderBoardScrpt : MonoBehaviour
{
    private void Start()
    {
        MainManager.Instance.UpdateTextList();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
