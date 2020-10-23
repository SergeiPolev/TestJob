using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel(int num)
    {
        SceneManager.LoadScene(num);
    }
    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
    }
    public void LoadFailScreen()
    {
        SceneManager.LoadScene("Lose");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void LoadLastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Last Level"));
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Last Level") + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
