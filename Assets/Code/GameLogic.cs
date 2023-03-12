using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

    public Renderer background;
    public Renderer ground;

    public int playerScore;

    public float textWobbleRange = 15;
    public float textWobbleSpeed = 3;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI introText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI gameOverText;

    private Vector3 _defaultControlsTextPosition;
    private Vector3 _defaultRestartTextPosition;

    private PlayerControls _playerControls;
    private InputAction _quitGameAction;
    private InputAction _restartGameAction;
    private InputAction _startGameAction;

    public bool IsPlayerDead { get; private set; }
    public bool IsGameStarted { get; private set; }

    private void Awake() {
        _playerControls = new PlayerControls();
        playerScoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        _defaultControlsTextPosition = controlsText.transform.position;
        _defaultRestartTextPosition = restartText.transform.position;
    }

    private void Update() {
        if (IsGameStarted) Cursor.visible = false;
        controlsText.transform.position = CalculateWobblePosition(_defaultControlsTextPosition);
        restartText.transform.position = CalculateWobblePosition(_defaultRestartTextPosition);

        if (IsPlayerDead) return;
        background.material.mainTextureOffset += Vector2.right * ((IsGameStarted ? 0.08f : 0.03f) * Time.deltaTime);
        ground.material.mainTextureOffset += Vector2.right * ((IsGameStarted ? 0.255f : 0.15f) * Time.deltaTime);
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
        IsPlayerDead = true;
        Cursor.visible = true;
        gameOverText.gameObject.SetActive(true);
    }

    [ContextMenu("Restart Game")]
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerScore = 0;
        IsPlayerDead = false;
        IsGameStarted = false;
        introText.gameObject.SetActive(true);
    }

    private void StartGame(InputAction.CallbackContext context) {
        if (IsGameStarted) return;
        IsGameStarted = true;
        introText.gameObject.SetActive(false);
        playerScoreText.gameObject.SetActive(true);
    }

    private Vector3 CalculateWobblePosition(Vector3 defaultPosition) {
        return defaultPosition + Vector3.up * (Mathf.Sin(Time.time * textWobbleSpeed) * textWobbleRange);
    }
}