using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject WinMenu;
    public GameObject OverMenu;

    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(this);
    }

    public void GameOver()
    {
        OverMenu.SetActive(true);
    }

    public void GameSuccess()
    {
        WinMenu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void BattleMode()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
