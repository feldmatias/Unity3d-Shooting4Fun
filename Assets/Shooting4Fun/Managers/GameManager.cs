using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas hudCanvas;
    public Canvas pauseMenuCanvas;
    public Canvas gameOverCanvas;
    public Canvas mapCanvas;

    private bool gamePaused = false;
    private bool gameEnded = false;

    private void Start()
    {
        LockMouse();
        hudCanvas.gameObject.SetActive(true);
        pauseMenuCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void TogglePausedGame()
    {
        if (gameEnded){
            return;
        }

        gamePaused = !gamePaused;

        hudCanvas.gameObject.SetActive(!gamePaused);
        pauseMenuCanvas.gameObject.SetActive(gamePaused);

        Time.timeScale = gamePaused ? 0 : 1;

        if (gamePaused) {
            UnlockMouse();
            mapCanvas.gameObject.SetActive(false);
        } else {
            LockMouse();
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(Scenes.MENU);
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Scenes.GAME);
    }

    public void ShowMap()
    {
        if (gamePaused || gameEnded) {
            return;
        }

        hudCanvas.gameObject.SetActive(false);
        mapCanvas.gameObject.SetActive(true);
    }

    public void HideMap()
    {
        if (gamePaused || gameEnded) {
            return;
        }

        hudCanvas.gameObject.SetActive(true);
        mapCanvas.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        gameEnded = true;
        gameOverCanvas.gameObject.SetActive(true);
        hudCanvas.gameObject.SetActive(false);
        pauseMenuCanvas.gameObject.SetActive(false);
        mapCanvas.gameObject.SetActive(false);
        Time.timeScale = 0;
        UnlockMouse();
    }
}
