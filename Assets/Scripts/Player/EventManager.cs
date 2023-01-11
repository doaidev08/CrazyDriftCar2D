using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EventManager : MonoBehaviour
{
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameMain");
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void ReplayGame() //Chơi lại sau khi thua
    {
        SceneManager.LoadScene("GameMain");
    }
    public void PauseGame()
    {

        if (!PlayerManager.isGamePaused && !PlayerManager.gameOver)
        {
            Time.timeScale = 0;
            PlayerManager.isGamePaused = true;
        }

    }

    public void ResumeGame() //Tiếp tục sau khi dừng
    {
        if (PlayerManager.isGamePaused)
        {
            Time.timeScale = 1;
            PlayerManager.isGamePaused = false;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
