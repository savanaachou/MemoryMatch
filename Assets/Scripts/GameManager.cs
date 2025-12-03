using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Managers
    public CardManager cardManager;
    public TimerController timerController;
    public UIManager uiManager;

    // Observer Pattern
    public event System.Action OnGameStarted;
    public event System.Action OnGameOver;
    public event System.Action OnGameWin;

    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        uiManager.ShowStartPanel();

        // Subscribe to external manager events
        timerController.OnTimerEnd += HandleGameOver;
        cardManager.OnAllPairsMatched += HandleGameWin;
    }

    public void StartGame()
    {
        gameStarted = true;

        uiManager.ShowGamePanel();
        timerController.StartTimer();
        cardManager.SetupCards();

        OnGameStarted?.Invoke();
    }

    public void RestartGame()
    {
        gameStarted = false;

        timerController.ResetTimer();
        cardManager.ClearCards();
        uiManager.ShowStartPanel();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    private void HandleGameOver()
    {
        if (!gameStarted) return;

        gameStarted = false;
        uiManager.ShowEndPanel("Time's up! Try Again!");
        OnGameOver?.Invoke();
    }

    private void HandleGameWin()
    {
        if (!gameStarted) return;

        gameStarted = false;
        uiManager.ShowEndPanel("You Win!");
        OnGameWin?.Invoke();
    }
}