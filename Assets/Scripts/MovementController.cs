using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]

public class MovementController : MonoBehaviour {
    [SerializeField] float runSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 wasdInput;
    private float xMovement;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // OnMovement needs to be called `On` whatever the InputAsset Name is.
    private void OnMovement(InputValue value) {
        wasdInput = value.Get<Vector2>();
        xMovement = wasdInput.x * runSpeed;
    }

    private void FixedUpdate() {
        if (xMovement != 0) {
            rb.velocity = new Vector2(xMovement, rb.velocity.y);
        }
    }
}
