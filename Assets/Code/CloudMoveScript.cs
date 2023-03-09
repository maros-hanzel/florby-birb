using UnityEngine;

public class CloudMoveScript : MonoBehaviour {

    public float minMoveSpeed = 5.5f;
    public float maxMoveSpeed = 7.5f;
    public float minWobbleAmplitude = 0.3f;
    public float maxWobbleAmplitude = 0.8f;

    public float wobbleSpeed = 3;

    private GameLogic _gameLogic;
    private float _initialYPosition;

    private float _limitX;

    private float _moveSpeed;
    private float _randomTimeOffset;
    private float _wobbleAmplitude;

    private void Start() {
        _gameLogic = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z
        ));
        _limitX = 0 - screenBounds.x - 5;
        _initialYPosition = transform.position.y;

        _randomTimeOffset = Random.Range(0f, 30f);

        _moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        _wobbleAmplitude = Random.Range(minWobbleAmplitude, maxWobbleAmplitude);
    }

    private void Update() {
        if (transform.position.x < _limitX) {
            Destroy(gameObject);
        }

        float finalMoveSpeed = _moveSpeed;
        if (_gameLogic.IsGameStarted && !_gameLogic.IsPlayerDead) {
            finalMoveSpeed *= 2;
        }

        Vector3 pos = transform.position;
        pos.x -= finalMoveSpeed * Time.deltaTime;
        pos.y = _initialYPosition + Mathf.Sin((Time.time + _randomTimeOffset) * wobbleSpeed) * _wobbleAmplitude;
        transform.position = pos;
    }
}