using UnityEngine;

public class GameState : IGameState
{
    private GameManager gameManager;

    public GameState(GameManager manager)
    {
        gameManager = manager;
    }

    public void Enter()
    {
        Debug.Log("Entering GameState");
        gameManager.uiManager.ShowGamePanel();
        gameManager.cardManager.SetupCards();
        gameManager.timerController.StartTimer();
    }

    public void Exit()
    {
        Debug.Log("Exiting GameState");
        gameManager.uiManager.HideGamePanel();
        gameManager.cardManager.ClearCards();
        gameManager.timerController.ResetTimer();
    }

    public void Update() { }
}