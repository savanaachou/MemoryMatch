using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject scorePanel;
    public GameObject selectPanel;
    public GameObject gamePanel;
    public GameObject endPanel;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI timeTakenText;

    // Show methods
    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
        scorePanel.SetActive(false);
        selectPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }
    
    public void ShowSorePanel()
    {
        startPanel.SetActive(false);
        scorePanel.SetActive(true);
        selectPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }
    
    public void ShowSelectPanel()
    {
        startPanel.SetActive(false);
        scorePanel.SetActive(false);
        selectPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    public void ShowGamePanel()
    {
        startPanel.SetActive(false);
        scorePanel.SetActive(false);
        selectPanel.SetActive(false);
        gamePanel.SetActive(true);
        endPanel.SetActive(false);
    }

    public void ShowEndPanel(string message, float? timeTaken = null)
    {
        startPanel.SetActive(false);
        scorePanel.SetActive(false);
        selectPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        
        resultText.text = message;

        if (timeTaken.HasValue)
        {
            timeTakenText.text = "Time Taken: " + Mathf.Round(timeTaken.Value) + "s";
            timeTakenText.gameObject.SetActive(true);
        }
        else
            timeTakenText.gameObject.SetActive(false);
    }

    public void HideStartPanel()
    {
        startPanel.SetActive(false);
    }

    public void HideGamePanel()
    {
        gamePanel.SetActive(false);
    }

    public void HideEndPanel()
    {
        endPanel.SetActive(false);
    }
    
    public void SetGrid_3x2()
    {
        GameManager.Instance.cardManager.selectedGridSize = GridSize.ThreeByTwo;
    }

    public void SetGrid_4x3()
    {
        GameManager.Instance.cardManager.selectedGridSize = GridSize.FourByThree;
    }

}