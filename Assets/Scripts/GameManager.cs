using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private IGameState currentState;

    // Managers
    public CardManager cardManager;
    public TimerController timerController;
    public UIManager uiManager;

    // Observer Pattern
    public event System.Action OnGameStarted;
    public event System.Action OnGameOver;
    public event System.Action OnGameWin;

    // private bool gameStarted = false;

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
        SetState(new StartState(this)); // show start panel initially

        // Subscribe to external manager events
        timerController.OnTimerEnd += HandleGameOver;
        cardManager.OnAllPairsMatched += HandleGameWin;
    }

    public void StartGame()
    {
        SetState(new GameState(this));
        OnGameStarted?.Invoke();
    }

    public void RestartGame()
    {
        SetState(new StartState(this));
    }

    private void HandleGameWin()
    {
        float elapsed = timerController.GetElapsedTime();
        SetState(new EndState(this, "You Win!", elapsed));
        OnGameWin?.Invoke();
    }

    private void HandleGameOver()
    {
        float elapsed = timerController.GetElapsedTime();
        SetState(new EndState(this, "Time’s up. Try Again!", elapsed));
        OnGameOver?.Invoke();
    }

    
    public void SetState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}