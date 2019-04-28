using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public Button startButton;
    public Button restartButton;
    public Button restartLevelButton;

    private GameObject finalScore;

    void Start() {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        if (startButton != null) {
            startButton.onClick.AddListener(StartGame);
        }
        if (restartButton != null) {
            restartButton.onClick.AddListener(RestartGame);
        }
        if (restartLevelButton != null) {
            restartLevelButton.onClick.AddListener(RestartLevel);
        }

        if (GameManager.instance != null && GameManager.instance.GetLevel() == 7) {
            GameObject.Find("Title").GetComponent<Text>().text = "Hurray! You win!";
        }
        finalScore = GameObject.Find("FinalScoreText");
        if (finalScore) {
            finalScore.GetComponent<Text>().text = "Score: " + GameManager.instance.GetPlayerMoneyPoints();
        }
    }

    void StartGame () {
        SceneManager.LoadScene("MainScene");
    }

    void RestartGame () {
        GameManager.instance.Restart();
        SceneManager.LoadScene("MainScene");
    }

    void RestartLevel () {
        GameManager.instance.RestartLevel();
        SceneManager.LoadScene("MainScene");
    }
}
