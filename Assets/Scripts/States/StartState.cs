using UnityEngine;

public class StartState : IGameState
{
    private GameManager gameManager;

    public StartState(GameManager manager)
    {
        gameManager = manager;
    }

    public void Enter()
    {
        Debug.Log("Entering StartState");
        gameManager.uiManager.ShowStartPanel();
    }

    public void Exit()
    {
        Debug.Log("Exiting StartState");
        gameManager.uiManager.HideStartPanel();
    }

    public void Update() { }
}