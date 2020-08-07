using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class MovementController : MonoBehaviour {
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpPower = 22.5f;
    [SerializeField] float climbSpeed = 5f;

    // Cached refs
    [SerializeField] InputActionAsset asset;

    private Rigidbody2D rb;
    private Animator anim;
    private float gravityScaleAtStart;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    private void FixedUpdate() {
        CheckIsRunning();
        CanClimbLadder();
        Jump();
        
    }

    private void Jump() {
        bool didTriggerJump = asset.FindAction("Jump").triggered;
        bool canJump = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbable"));
        if (!canJump || !didTriggerJump) { return; }

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

    private void CanClimbLadder() {

        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbable"))) {
            anim.SetBool("isClimbing", false);
            rb.gravityScale = gravityScaleAtStart;
            return;
        }

        rb.gravityScale = 0f;

        var y = asset.FindAction("Movement").ReadValue<Vector2>().y * climbSpeed;
        bool isMovingOnY = Mathf.Abs(y) > Mathf.Epsilon;

        anim.SetBool("isClimbing", isMovingOnY);
        rb.velocity = new Vector2(rb.velocity.x, isMovingOnY ? y : 0);
    }
}
