using UnityEngine;

public class ScoreTriggerScript : MonoBehaviour {

    private GameLogic _gameLogic;

    private void Start() {
        _gameLogic = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) _gameLogic.AddScore();
    }
}