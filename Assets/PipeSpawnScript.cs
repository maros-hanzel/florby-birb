using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour {

    public GameObject pipePair;

    public float spawnRate = 2;

    private float timer = 0;
    private float minSpawnBound;
    private float maxSpawnBound;

    void Start() {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z
        ));

        float offset = 5;
        minSpawnBound = transform.position.y - screenBounds.y + offset;
        maxSpawnBound = transform.position.y + screenBounds.y - offset;
    }

    void Update() {
        if ((timer += Time.deltaTime) >= spawnRate) {
            timer = 0;

            float yPos = Random.Range(minSpawnBound, maxSpawnBound);
            Vector3 pipePairPos = new Vector3(transform.position.x, yPos, 0);
            Instantiate(pipePair, pipePairPos, transform.rotation);
        }
    }
}
