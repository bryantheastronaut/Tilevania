using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour {
    [Tooltip("Game units in second")]
    [SerializeField] float scrollSpeed = .2f;

    void Update() {
        transform.Translate(new Vector2(0, scrollSpeed * Time.deltaTime));
    }
}
