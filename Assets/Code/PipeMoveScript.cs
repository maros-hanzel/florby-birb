using UnityEngine;

public class PipeMoveScript : MonoBehaviour {

    public BoxCollider2D boxCollider;
    public float moveSpeed = 10;
    private GameLogic _gameLogic;

    private float _limitX;

    private void Start() {
        _gameLogic = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z
        ));
        _limitX = 0 - screenBounds.x - boxCollider.size.x;
    }

    private void Update() {
        if (_gameLogic.IsPlayerDead) return;
        if (transform.position.x < _limitX) {
            Destroy(gameObject);
        }

        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);
    }
}