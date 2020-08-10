using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {
    int startingSceneIdx;
    private void Awake() {
        int numScenes = FindObjectsOfType<ScenePersist>().Length;
        int buildIdx = SceneManager.GetActiveScene().buildIndex;

        if (numScenes > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        startingSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update() {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        if (startingSceneIdx != currentSceneIdx) { Destroy(gameObject); }
    }
}
