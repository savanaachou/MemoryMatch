using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float maxTime = 60f;
    public TextMeshProUGUI timerText;

    public event System.Action OnTimerEnd;

    private float timer;
    private bool running = false;

    private void Update()
    {
        if (!running) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            running = false;
            timer = 0;
            UpdateTimerText();
            OnTimerEnd?.Invoke();
        }
    }

    public void StartTimer()
    {
        timer = maxTime;
        running = true;
        UpdateTimerText();
    }

    public void ResetTimer()
    {
        running = false;
        timer = maxTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time Left: " + Mathf.Round(timer) + "s";
    }
}