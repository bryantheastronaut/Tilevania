using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelExitSlowmo = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.GetComponent<MovementController>();
        if (player) {
            if (player.CheckPlayerIsAlive()) {
                StartCoroutine(GoNextLevel());
            }
        }
    }

    IEnumerator GoNextLevel() {
        Time.timeScale = levelExitSlowmo;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
}
