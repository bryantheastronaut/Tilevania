using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]

public class MovementController : MonoBehaviour {
    [SerializeField] float runSpeed = 5f;
    [SerializeField] InputActionAsset asset;

    private InputAction inputAction;
    private Rigidbody2D rb;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputAction = asset.FindAction("Movement");
    }

    private void FixedUpdate() {
        var x = inputAction.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(x * runSpeed, rb.velocity.y);
    }
}
