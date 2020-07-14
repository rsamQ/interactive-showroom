using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void ClickClimateWorld()
    {
        SceneManager.UnloadScene("MainMenuScene");
        SceneManager.LoadScene("WorldScene");

    }

    public void ClickQuiz()
    {
        SceneManager.UnloadScene("MainMenuScene");
        SceneManager.LoadScene("GameScene");

    }

     public void ClickVideo()
    {
        SceneManager.UnloadScene("MainMenuScene");
        SceneManager.LoadScene("IntroScene");

    }

    public void ClickImpressum()
    {
        SceneManager.UnloadScene("MainMenuScene");
        SceneManager.LoadScene("Impressum");

    }

    public void ClickExitImpressum()
    {
        SceneManager.UnloadScene("Impressum");
        SceneManager.LoadScene("MainMenuScene");

    }
}
