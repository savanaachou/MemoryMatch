using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject endPanel;

    public TextMeshProUGUI resultText;

    // Show methods
    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    public void ShowGamePanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        endPanel.SetActive(false);
    }

    public void ShowEndPanel(string message)
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        resultText.text = message;
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
}