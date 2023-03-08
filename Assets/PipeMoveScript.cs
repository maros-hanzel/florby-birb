using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour {

    public float moveSpeed = 5;

    public BoxCollider2D boxCollider;

    private float limitX;

    private void Start() {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z
        ));
        limitX = 0 - screenBounds.x - boxCollider.size.x;
    }

    void Update() {
        if (transform.position.x < limitX) {
            Destroy(gameObject);
        }
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
