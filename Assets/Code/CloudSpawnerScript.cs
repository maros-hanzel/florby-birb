using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour {

    public GameObject cloud;

    public float initialSpawnCount = 10;
    public float spawnRate = 2;

    private Vector2 _screenBounds;

    private float _timer;

    private void Start() {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z
        ));

        for (int i = 0; i < initialSpawnCount; i++) {
            SpawnCloud(Random.Range(-_screenBounds.x, _screenBounds.x));
        }

        _timer = spawnRate;
    }

    private void Update() {
        if ((_timer += Time.deltaTime) < spawnRate) return;
        _timer = 0;

        SpawnCloud(_screenBounds.x + 5f);
    }

    private void SpawnCloud(float xPos) {
        Vector3 spawnPos = new(
            xPos,
            Random.Range(0, _screenBounds.y),
            Random.Range(1f, 20f)
        );
        Instantiate(cloud, spawnPos, transform.rotation);
    }
}