using UnityEngine;

public class EndState : IGameState
{
    private GameManager gameManager;
    private string resultText;

    public EndState(GameManager manager, string result)
    {
        gameManager = manager;
        resultText = result;
    }

    public void Enter()
    {
        Debug.Log("Entering EndState");

        // Get time taken from timer controller (elapsed time counting UP)
        float finalTime = gameManager.timerController.GetTimeTaken();

        // Save the score for the selected grid size
        HighScoreManager.Instance.SaveScore(
            gameManager.cardManager.selectedGridSize,
            finalTime
        );

        // Show the end screen with the correct time
        gameManager.uiManager.ShowEndPanel(resultText, finalTime);
    }

    public void Exit()
    {
        Debug.Log("Exiting EndState");
        gameManager.uiManager.HideEndPanel();
    }

    public void Update() { }
}