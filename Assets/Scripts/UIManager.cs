using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject endPanel;

    public TextMeshProUGUI resultText;

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
        gamePanel.SetActive(false);
        startPanel.SetActive(false);

        endPanel.SetActive(true);
        resultText.text = message;
    }
}