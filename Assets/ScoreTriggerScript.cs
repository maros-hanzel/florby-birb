using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerScript : MonoBehaviour {

    private GameLogic gameLogic;

    void Start() {
        gameLogic = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameLogic>();
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) {
            gameLogic.addScore();
        }
    }
}
