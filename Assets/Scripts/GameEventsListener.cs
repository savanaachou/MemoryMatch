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
        while (GameManagerCard.Instance == null)
            yield return null;

        GameManagerCard.Instance.OnGameStarted += HandleGameStarted;
        GameManagerCard.Instance.OnGameOver += HandleGameOver;
        GameManagerCard.Instance.OnGameWin += HandleGameWin;
    }

    private void OnDisable()
    {
        if (GameManagerCard.Instance == null) return;

        GameManagerCard.Instance.OnGameStarted -= HandleGameStarted;
        GameManagerCard.Instance.OnGameOver -= HandleGameOver;
        GameManagerCard.Instance.OnGameWin -= HandleGameWin;
    }


    void HandleGameStarted() => Debug.Log("Game Started!");
    void HandleGameOver()   => Debug.Log("Game Over!");
    void HandleGameWin()    => Debug.Log("Player Won!");
}