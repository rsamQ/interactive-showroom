using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void ClickClimateWorld()
    {
        SceneManager.UnloadSceneAsync("MainMenuScene");
        SceneManager.LoadSceneAsync("WorldScene");

    }

    public void ClickQuiz()
    {
        SceneManager.UnloadSceneAsync("MainMenuScene");
        SceneManager.LoadSceneAsync("GameScene");

    }

     public void ClickVideo()
    {
        SceneManager.UnloadSceneAsync("MainMenuScene");
        SceneManager.LoadSceneAsync("IntroScene");

    }

    public void ClickImpressum()
    {
        SceneManager.UnloadSceneAsync("MainMenuScene");
        SceneManager.LoadSceneAsync("Impressum");

    }

    public void ClickExitImpressum()
    {
        SceneManager.UnloadSceneAsync("Impressum");
        SceneManager.LoadSceneAsync("MainMenuScene");

    }
}
