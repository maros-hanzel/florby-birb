using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirbScript : MonoBehaviour {

    public Rigidbody2D body;

    public GameLogic gameLogic;

    public float flapStrength;

    private PlayerControls playerControls;
    private InputAction flap;

    private GameObject wing;

    private Vector3 defaultWingPositionShift;

    private float defaultGravityScale;

    private void Awake() {
        playerControls = new PlayerControls();
        defaultGravityScale = body.gravityScale;
    }

    private void OnEnable() {
        flap = playerControls.Player.Flap;
        flap.Enable();
        flap.performed += Flap;
        flap.canceled += ResetFlap;
    }

    private void OnDisable() {
        flap.Disable();
    }

    void Start() {
        body.gravityScale = 0;
        wing = GameObject.Find(gameObject.name + "/Wing");
        defaultWingPositionShift = wing.transform.position - transform.position;
    }

    private void Update() {
        if (!gameLogic.gameStarted) return;
        transform.Rotate(Vector3.forward, -10 * Time.deltaTime * 10);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        gameLogic.gameOver();
    }

    private void flapWing(bool flapped) {
        Transform wingT = wing.transform;
        if (flapped) {
            transform.eulerAngles = Vector3.forward * 30;
            wingT.RotateAround(wingT.position + wingT.up * -0.25f, wingT.right, 180);
        } else {
            wingT.position = (Vector3)body.position + transform.right * -1 * defaultWingPositionShift.magnitude;
            wingT.rotation = transform.rotation;
        }
    }

    private void Flap(InputAction.CallbackContext context) {
        if (gameLogic.isDead) return;

        body.gravityScale = defaultGravityScale;
        body.velocity = Vector2.up * flapStrength;
        flapWing(true);
    }

    private void ResetFlap(InputAction.CallbackContext context) {
        if (gameLogic.isDead) return;
        flapWing(false);
    }

}
