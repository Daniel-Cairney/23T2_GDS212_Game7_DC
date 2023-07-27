using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadMain()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadDeath()
    {
        SceneManager.LoadScene("Death Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TutorialScene()
    {
        SceneManager.LoadScene("Tutorial Scene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
