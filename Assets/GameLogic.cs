using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogic : MonoBehaviour {

    public int playerScore;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI introText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI gameOverText;

    public bool isDead { get; private set; } = false;
    public bool gameStarted { get; private set; } = false;

    private PlayerControls _playerControls;
    private InputAction _startGameAction;
    private InputAction _restartGameAction;
    private InputAction _quitGameAction;

    private Vector3 controlsTextInitialPosition;

    private void Awake() {
        _playerControls = new PlayerControls();
        Time.timeScale = 0;
        Cursor.visible = false;
        playerScoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        controlsTextInitialPosition = controlsText.transform.position;
    }

    private void OnEnable() {
        _startGameAction = _playerControls.UI.StartGame;
        _startGameAction.performed += StartGame;
        _startGameAction.Enable();

        _restartGameAction = _playerControls.UI.RestartGame;
        _restartGameAction.performed += _ => RestartGame();
        _restartGameAction.Enable();

        _quitGameAction = _playerControls.UI.QuitGame;
        _quitGameAction.performed += _ => Application.Quit();
        _quitGameAction.Enable();
    }

    private void OnDisable() {
        _startGameAction.Disable();
        _restartGameAction.Disable();
        _quitGameAction.Disable();
    }

    public void AddScore() {
        playerScore += 1;
        Debug.Log("score = " + playerScore);
        playerScoreText.text = playerScore.ToString();
    }

    public void GameOver() {
        Debug.Log("Game Over");
        isDead = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        gameOverText.gameObject.SetActive(true);
    }

    [ContextMenu("Restart Game")]
    public void RestartGame() {
        playerScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isDead = false;
        gameStarted = false;
        introText.gameObject.SetActive(true);
    }

    private void StartGame(InputAction.CallbackContext context) {
        if (gameStarted) return;
        gameStarted = true;
        introText.gameObject.SetActive(false);
        playerScoreText.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
