using UnityEngine;
using UnityEngine.InputSystem;

public class BirbScript : MonoBehaviour {
    public Rigidbody2D birbRigidBody;

    public GameLogic gameLogic;

    public Animator animator;

    public float flapStrength = 22f;

    private float _defaultGravityScale;
    private InputAction _flapAction;

    private PlayerControls _playerControls;

    private void Awake() {
        _playerControls = new PlayerControls();
        _defaultGravityScale = birbRigidBody.gravityScale;
        birbRigidBody.gravityScale = 0;
        animator.SetBool("IsDead", false);
    }

    private void Update() {
        if (!gameLogic.IsGameStarted || gameLogic.IsPlayerDead) return;
        transform.Rotate(Vector3.forward, -10 * Time.deltaTime * 10);
        animator.SetBool("IsGameStarted", gameLogic.IsGameStarted);
    }

    private void OnEnable() {
        _flapAction = _playerControls.Player.Flap;
        _flapAction.Enable();
        _flapAction.performed += Flap;
        _flapAction.canceled += _ => animator.ResetTrigger("Flap");
    }

    private void OnDisable() {
        _flapAction.Disable();
    }

    private void OnCollisionEnter2D() {
        gameLogic.GameOver();
        animator.SetBool("IsDead", true);
    }

    private void Flap(InputAction.CallbackContext context) {
        if (gameLogic.IsPlayerDead) return;

        birbRigidBody.gravityScale = _defaultGravityScale;
        birbRigidBody.velocity = Vector2.up * flapStrength;
        transform.eulerAngles = Vector3.forward * 20;
        animator.SetTrigger("Flap");
    }
}