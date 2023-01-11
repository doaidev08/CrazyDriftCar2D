using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject startingText;
    public static bool isGameStarted;
    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI newRecordText;

    public static bool isGamePaused;
    public bool flagCheckNewScore = false;


    void Start()
    {
        score = 0;
        Time.timeScale = 1;
        gameOver = isGameStarted = isGamePaused = false;

    }

    void Update()
    {
        scoreText.text = score.ToString();


        if (gameOver)
        {
            Time.timeScale = 0;
            if (score > PlayerPrefs.GetInt("HighScore", 0)){

                newRecordText.text = score.ToString();
                PlayerPrefs.SetInt("HighScore", score);
                flagCheckNewScore= true;
            }else{
                newRecordText.text = score.ToString();
            }

            if (flagCheckNewScore == false)
            {
                gameOverPanel.SetActive(true);
                Destroy(gameObject);
            }
        }


        if (Input.GetMouseButtonDown(0) && !isGameStarted) //Tap to play
        {
            isGameStarted = true;
            Destroy(startingText);
        
        }
    }
}
