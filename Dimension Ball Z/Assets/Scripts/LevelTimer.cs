using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timer;
    private bool _isRunning;
    private double _elapsedTime;
    public float milliseconds, seconds, minutes;

    private void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.extraBalls == 0)
            StopTimer();
        if (!_isRunning) return;
        _elapsedTime += Time.deltaTime;
        minutes = (int)(_elapsedTime / 60f);
        seconds = (int)(_elapsedTime % 60f);
        milliseconds = (int)(_elapsedTime * 1000f) % 100;
        timer.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
    private void StartTimer() {
        _isRunning = true;
    }
    private void StopTimer() {
        _isRunning = false;
    }

    public void ResetTimer() {
        _elapsedTime = 0;
    }
    

}
