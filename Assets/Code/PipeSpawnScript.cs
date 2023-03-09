using UnityEngine;

public class PipeSpawnScript : MonoBehaviour {

    private const float MinDistanceFromBounds = 5;

    public GameObject pipePair;
    public GameLogic gameLogic;
    public float spawnRate = 2;

    private float _maxSpawnBound;
    private float _minSpawnBound;
    private float _pipeSpawnPosX;

    private float _timer;

    private void Start() {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z
        ));

        _pipeSpawnPosX = screenBounds.x + 10;

        float positionY = transform.position.y;
        _minSpawnBound = positionY - screenBounds.y + MinDistanceFromBounds;
        _maxSpawnBound = positionY + screenBounds.y - MinDistanceFromBounds;
    }

    private void Update() {
        if (!gameLogic.IsGameStarted || (_timer += Time.deltaTime) < spawnRate) return;
        _timer = 0;

        float yPos = Random.Range(_minSpawnBound, _maxSpawnBound);
        Vector3 pipePairPos = new(_pipeSpawnPosX, yPos, 0);
        Instantiate(pipePair, pipePairPos, transform.rotation);
    }
}