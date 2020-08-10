using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinValue = 10;

    private void OnTriggerEnter2D(Collider2D collision) {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
        GameSession gs = FindObjectOfType<GameSession>();
        gs.AddScore(coinValue);
    }
}
