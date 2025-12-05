using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanelUI : MonoBehaviour
{
    public TextMeshProUGUI grid4x3_1;
    public TextMeshProUGUI grid4x3_2;
    public TextMeshProUGUI grid4x3_3;

    public TextMeshProUGUI grid3x2_1;
    public TextMeshProUGUI grid3x2_2;
    public TextMeshProUGUI grid3x2_3;

    public void OnEnable()
    {
        LoadAndDisplayScores();
    }

    public void LoadAndDisplayScores()
    {
        // 4x3
        List<float> scores43 = HighScoreManager.Instance.LoadScores(GridSize.FourByThree);

        grid4x3_1.text = scores43.Count > 0 ? scores43[0].ToString("0.00") + "s" : "---";
        grid4x3_2.text = scores43.Count > 1 ? scores43[1].ToString("0.00") + "s" : "---";
        grid4x3_3.text = scores43.Count > 2 ? scores43[2].ToString("0.00") + "s" : "---";

        // 3x2
        List<float> scores32 = HighScoreManager.Instance.LoadScores(GridSize.ThreeByTwo);

        grid3x2_1.text = scores32.Count > 0 ? scores32[0].ToString("0.00") + "s" : "---";
        grid3x2_2.text = scores32.Count > 1 ? scores32[1].ToString("0.00") + "s" : "---";
        grid3x2_3.text = scores32.Count > 2 ? scores32[2].ToString("0.00") + "s" : "---";
    }
}