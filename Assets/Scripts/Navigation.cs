using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void GoToFirstLevel()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void GoToSecondLevel()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReplayCurrentLevel()
    {
        string level = PlayerPrefs.GetString("Level");
        SceneManager.LoadScene(level);

    }

 
    public void GoToWinningScreen()
    {
        SceneManager.LoadScene("Win");
    }

    public void ChooseLevelScreen()
    {
        SceneManager.LoadScene("ChooseLevel");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
