using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas playCanvas;
    public Canvas creditCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        mainCanvas.enabled = false;
        playCanvas.enabled = true;
        creditCanvas.enabled = false;
    }
    public void Credits()
    {
        mainCanvas.enabled = false;
        playCanvas.enabled = false;
        creditCanvas.enabled = true;
    }

    public void ReturnToMain()
    {
        mainCanvas.enabled = true;
        playCanvas.enabled = false;
        creditCanvas.enabled = false;
    }

}
