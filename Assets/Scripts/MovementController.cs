using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]

public class MovementController : MonoBehaviour {
    // Config
    [SerializeField] float runSpeed = 5f;

    // Cached refs
    [SerializeField] InputActionAsset asset;
    private InputAction inputAction;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputAction = asset.FindAction("Movement");

        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        var x = inputAction.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(x * runSpeed, rb.velocity.y);
        CheckIsRunning();
    }

    private void CheckIsRunning() {
        bool isMovingOnX = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", isMovingOnX);
        if (isMovingOnX) { FlipSprite(); }
    }

    private void FlipSprite() {
        // if player is moving horizontally, we need to reverse current scaling of x axis
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    }
}
