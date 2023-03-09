using UnityEngine;
using UnityEngine.InputSystem;

public class BirbScript : MonoBehaviour {
    public Rigidbody2D birbRigidBody;

    public GameLogic gameLogic;

    public float flapStrength = 22f;

    private float _defaultGravityScale;
    private Vector3 _defaultWingPositionShift;
    private InputAction _flapAction;

    private PlayerControls _playerControls;

    private GameObject _wing;

    private void Awake() {
        _playerControls = new PlayerControls();
        _defaultGravityScale = birbRigidBody.gravityScale;
    }

    private void Start() {
        birbRigidBody.gravityScale = 0;
        _wing = GameObject.Find(gameObject.name + "/Wing");
        _defaultWingPositionShift = _wing.transform.position - transform.position;
    }

    private void Update() {
        if (!gameLogic.IsGameStarted || gameLogic.IsPlayerDead) return;
        transform.Rotate(Vector3.forward, -10 * Time.deltaTime * 10);
    }

    private void OnEnable() {
        _flapAction = _playerControls.Player.Flap;
        _flapAction.Enable();
        _flapAction.performed += Flap;
        _flapAction.canceled += ResetFlap;
    }

    private void OnDisable() {
        _flapAction.Disable();
    }

    private void OnCollisionEnter2D() {
        gameLogic.GameOver();
    }

    private void FlapWing(bool flapped) {
        Transform wingT = _wing.transform;
        Transform cachedTransform = transform;

        if (flapped) {
            cachedTransform.eulerAngles = Vector3.forward * 30;
            wingT.RotateAround(wingT.position + wingT.up * -0.25f, wingT.right, 180);
        } else {
            wingT.position = (Vector3)birbRigidBody.position +
                cachedTransform.right * -1 * _defaultWingPositionShift.magnitude;
            wingT.rotation = cachedTransform.rotation;
        }
    }

    private void Flap(InputAction.CallbackContext context) {
        if (gameLogic.IsPlayerDead) return;

        birbRigidBody.gravityScale = _defaultGravityScale;
        birbRigidBody.velocity = Vector2.up * flapStrength;
        FlapWing(true);
    }

    private void ResetFlap(InputAction.CallbackContext context) {
        if (gameLogic.IsPlayerDead) return;
        FlapWing(false);
    }
}