using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasController : MonoBehaviour
{
    CharacterControls action;
    public Canvas PauseCanvas;
    public Canvas WinCanvas;
    public Canvas LoseCanvas;
    public GameObject Player;
    bool paused = false;

    private void Awake()
    {
        action = new CharacterControls();
    }

    private void OnEnable()
    {
        action.Enable();    
    }
    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    void DeterminePause()
    {
        if (paused)
            ResumeGame();
        else
            PauseGame();    
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        WinCanvas.enabled = true;
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        LoseCanvas.enabled = true;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        PauseCanvas.enabled = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        PauseCanvas.enabled = false;
    }

    public void MainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
