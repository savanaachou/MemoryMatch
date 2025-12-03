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
        gameManager.uiManager.ShowEndPanel(resultText);
    }

    public void Exit()
    {
        Debug.Log("Exiting EndState");
        gameManager.uiManager.HideEndPanel();
    }

    public void Update() { }
}