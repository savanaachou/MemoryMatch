using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private string GetKey(GridSize size)
    {
        return size switch
        {
            GridSize.FourByThree => "HS_4x3",
            GridSize.ThreeByTwo => "HS_3x2",
            _ => "HS_4x3"
        };
    }

    public List<float> LoadScores(GridSize size)
    {
        string key = GetKey(size);

        if (!PlayerPrefs.HasKey(key))
            return new List<float>();

        string data = PlayerPrefs.GetString(key);

        List<float> scores = new List<float>();
        foreach (string s in data.Split(','))
        {
            if (float.TryParse(s, out float value))
                scores.Add(value);
        }

        return scores;
    }

    public void SaveScore(GridSize size, float newScore)
    {
        List<float> scores = LoadScores(size);

        // add new score and sort
        scores.Add(newScore);
        scores.Sort();

        // keep only top 3
        if (scores.Count > 3)
            scores = scores.GetRange(0, 3);

        // save back
        string result = string.Join(",", scores);
        PlayerPrefs.SetString(GetKey(size), result);
        PlayerPrefs.Save();
    }
}