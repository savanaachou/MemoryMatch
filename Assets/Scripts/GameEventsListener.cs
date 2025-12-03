using System.Collections;
using UnityEngine;

public class GameEventsListener : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitForManager());
    }

    private IEnumerator WaitForManager()
    {
        // wait until GameManagerCard.Instance is available
        while (GameManager.Instance == null)
            yield return null;

        GameManager.Instance.OnGameStarted += HandleGameStarted;
        GameManager.Instance.OnGameOver += HandleGameOver;
        GameManager.Instance.OnGameWin += HandleGameWin;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnGameStarted -= HandleGameStarted;
        GameManager.Instance.OnGameOver -= HandleGameOver;
        GameManager.Instance.OnGameWin -= HandleGameWin;
    }


    void HandleGameStarted() => Debug.Log("Game Started!");
    void HandleGameOver()   => Debug.Log("Game Over!");
    void HandleGameWin()    => Debug.Log("Player Won!");
}