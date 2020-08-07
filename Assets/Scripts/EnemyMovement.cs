using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 1f;
    private Rigidbody2D rb;

    private BoxCollider2D[] edgeColliders;
     
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0);
        edgeColliders = GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        var currentMovement = rb.velocity;
        foreach (BoxCollider2D collider in edgeColliders) {
            if (!collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
                currentMovement.x = currentMovement.x * -1;
                break;
            }
        }
        rb.velocity = currentMovement;
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
    }
}
