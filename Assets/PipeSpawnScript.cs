using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour {

    public GameObject pipePair;

    public float spawnRate = 2;

    private const float offset = 5;

    private float _timer;
    private float _minSpawnBound;
    private float _maxSpawnBound;
    private float _pipeSpawnPosX;

    private void Start() {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z
        ));

        _pipeSpawnPosX = screenBounds.x + 10;

        float positionY = transform.position.y;
        _minSpawnBound = positionY - screenBounds.y + offset;
        _maxSpawnBound = positionY + screenBounds.y - offset;
    }

    private void Update() {
        if ((_timer += Time.deltaTime) < spawnRate) return;

        _timer = 0;

        float yPos = Random.Range(_minSpawnBound, _maxSpawnBound);
        Vector3 pipePairPos = new (_pipeSpawnPosX, yPos, 0);
        Instantiate(pipePair, pipePairPos, transform.rotation);
    }
}