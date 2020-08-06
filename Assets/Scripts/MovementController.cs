using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]

public class MovementController : MonoBehaviour {
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpPower = 15f;

    // Cached refs
    [SerializeField] InputActionAsset asset;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        CheckIsRunning();

        bool didTriggerJump = asset.FindAction("Jump").triggered;
        if (didTriggerJump) { Jump(); }
    }

    private void Jump() {
        Vector2 jumpVelocityToAdd = new Vector2(0f, jumpPower);
        rb.velocity += jumpVelocityToAdd;
    }

    private void CheckIsRunning() {
        var x = asset.FindAction("Movement").ReadValue<Vector2>().x * runSpeed;
        rb.velocity = new Vector2(x, rb.velocity.y);
        FlipSprite();        
    }

    private void FlipSprite() {
        bool isMovingOnX = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", isMovingOnX);

        if (isMovingOnX) {
            // if player is moving horizontally, we need to reverse current scaling of x axis
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
