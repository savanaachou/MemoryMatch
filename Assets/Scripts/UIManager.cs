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

    // --- SHOW METHODS ---

    public void ShowStartPanel()
    {
        ActivateOnly(startPanel);
    }

    public void ShowScorePanel()
    {
        ActivateOnly(scorePanel);

        // Refresh UI each time the panel opens
        ScorePanelUI ui = scorePanel.GetComponent<ScorePanelUI>();
        if (ui != null)
            ui.LoadAndDisplayScores();
    }

    public void ShowSelectPanel()
    {
        ActivateOnly(selectPanel);
    }

    public void ShowGamePanel()
    {
        ActivateOnly(gamePanel);
    }

    public void ShowEndPanel(string message, float? timeTaken = null)
    {
        ActivateOnly(endPanel);

        resultText.text = message;

        if (timeTaken.HasValue)
        {
            timeTakenText.text = "Time Taken: " + timeTaken.Value.ToString("0.00") + "s";
            timeTakenText.gameObject.SetActive(true);
        }
        else
        {
            timeTakenText.gameObject.SetActive(false);
        }
    }

    // --- HIDE METHODS ---

    public void HideStartPanel() => startPanel.SetActive(false);
    public void HideGamePanel() => gamePanel.SetActive(false);
    public void HideEndPanel() => endPanel.SetActive(false);

    // --- GRID SIZE BUTTONS ---

    public void SetGrid_3x2()
    {
        GameManager.Instance.cardManager.selectedGridSize = GridSize.ThreeByTwo;
    }

    public void SetGrid_4x3()
    {
        GameManager.Instance.cardManager.selectedGridSize = GridSize.FourByThree;
    }

    // --- HELPER: activates only this one panel ---

    private void ActivateOnly(GameObject panel)
    {
        startPanel.SetActive(false);
        scorePanel.SetActive(false);
        selectPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);

        panel.SetActive(true);
    }
}
