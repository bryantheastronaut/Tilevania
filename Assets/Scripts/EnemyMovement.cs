using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 1f;
    private Rigidbody2D rb;

    private BoxCollider2D enemyFrontCollider;
     
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0);
        enemyFrontCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (IsFacingRight()) {
            rb.velocity = new Vector2(moveSpeed, 0);
        } else {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    private bool IsFacingRight() {
        return Mathf.Sign(transform.localScale.x) > 0;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1);
    }
}
