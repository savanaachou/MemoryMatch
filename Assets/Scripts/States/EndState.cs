using UnityEngine;

public class EndState : IGameState
{
    private GameManager gameManager;
    private string resultText;
    private float? timeTaken;

    public EndState(GameManager manager, string result, float? timeTaken = null)
    {
        gameManager = manager;
        resultText = result;
        this.timeTaken = timeTaken;
    }

    public void Enter()
    {
        Debug.Log("Entering EndState");
        gameManager.uiManager.ShowEndPanel(resultText, timeTaken);
    }

    public void Exit()
    {
        Debug.Log("Exiting EndState");
        gameManager.uiManager.HideEndPanel();
    }

    public void Update() { }
}