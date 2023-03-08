using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogic : MonoBehaviour {

    public int playerScore = 0;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI introText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI gameOverText;

    public bool isDead { get; private set; } = false;
    public bool gameStarted { get; private set; } = false;

    private PlayerControls playerControls;
    private InputAction startGameAction;
    private InputAction restartGameAction;

    private Vector3 controlsTextInitialPosition;

    private void Awake() {
        playerControls = new PlayerControls();
        Time.timeScale = 0;
        Cursor.visible = false;
        playerScoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        controlsTextInitialPosition = controlsText.transform.position;
    }

    private void OnEnable() {
        startGameAction = playerControls.UI.StartGame;
        startGameAction.performed += startGame;
        startGameAction.Enable();

        restartGameAction = playerControls.UI.RestartGame;
        restartGameAction.performed += context => restartGame();
        restartGameAction.Enable();
    }

    private void OnDisable() {
        startGameAction.Disable();
        restartGameAction.Disable();
    }

    public void addScore() {
        playerScore += 1;
        Debug.Log("score = " + playerScore);
        playerScoreText.text = playerScore.ToString();
    }

    public void gameOver() {
        Debug.Log("Game Over");
        isDead = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        gameOverText.gameObject.SetActive(true);
    }

    [ContextMenu("Restart Game")]
    public void restartGame() {
        playerScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isDead = false;
        gameStarted = false;
        introText.gameObject.SetActive(true);
    }

    private void startGame(InputAction.CallbackContext context) {
        if (gameStarted) return;
        gameStarted = true;
        introText.gameObject.SetActive(false);
        Time.timeScale = 1;
        playerScoreText.gameObject.SetActive(true);
    }
}
