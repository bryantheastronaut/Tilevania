using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private int currentScore = 0;

    private void Awake() {
        if (FindObjectsOfType(GetType()).Length > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    private void Start() {
        UpdateScore();
        UpdateLives();
    }

    private void Update() {
        bool isMainMenu = SceneManager.GetActiveScene().name == "Main Menu";
        if (isMainMenu) { Destroy(gameObject); }
    }

    public void ProcessPlayerDeath() {
        if (playerLives > 1) {
            playerLives--;
            int currentBuildIdx = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentBuildIdx);
        } else {
            SceneManager.LoadScene("Main Menu");
        }
        UpdateLives();
    }

    public void AddScore(int score) {
        currentScore += score;
        UpdateScore();
    }

    private void UpdateScore() { scoreText.text = "Score " + currentScore.ToString(); }
    private void UpdateLives() { livesText.text = playerLives.ToString() + " lives"; }

}
