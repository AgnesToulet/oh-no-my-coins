using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;
    public bool doingSetup;

    private Text levelText;
    private Text pointsText;
    private Text tipsText;
    private GameObject levelImage;
    private GameObject restartScreen;
    private Text finalScoreText;
    private int level = 0;
    private int playerMoneyPoints = 0;

    private int lastMoneyAdded = 0;

    private string[] tips = {
        "You can jump to see the whole map by pressing <space>.",
        "You can go through walls to access closed labyrinth parts.",
        "Beware the cops! Find the trapdoors to hide from them.",
        "There is not always trapdoor in the level, sometimes you need to find another way to lose the cops...",
        "Just run!",
        "There is always one chest per level, will you find it?",
        "Have fun!"
    };

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
    }

    void Update () {    
        if (Input.GetKeyDown(KeyCode.Return) && doingSetup) {
            HideLevelImage();
        }
    }

    void OnEnable () {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable () {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (scene.name == "MainScene") {
            level++;
            InitGame();
        }
    }

    void InitGame () {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Robbery " + level;
        pointsText = GameObject.Find("PointsText").GetComponent<Text>();
        pointsText.text = "Total points: " + playerMoneyPoints;
        tipsText = GameObject.Find("TipsText").GetComponent<Text>();
        tipsText.text = "Tip: " + (level <= 7 ? tips[level-1] : tips[5]);
        levelImage.SetActive(true);

        boardScript.SetupScene(level);
    }

    private void HideLevelImage() {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void Restart () {
        level = 0;
        playerMoneyPoints = 0;
        lastMoneyAdded = 0;
    }

    public void RestartLevel () {
        level -= 1;
        playerMoneyPoints -= lastMoneyAdded;
        lastMoneyAdded = 0;
    }

    public void GameOver () {
        SceneManager.LoadScene("RestartScene");
    }

    public int GetLevel () {
        return level;
    }

    public int GetPlayerMoneyPoints () {
        return playerMoneyPoints;
    }

    public void AddPlayerMoneyPoints (int money) {
        lastMoneyAdded = money;
        playerMoneyPoints += money;
    }
}
