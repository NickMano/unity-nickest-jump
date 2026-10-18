using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int currentScore { get; private set; }
    public int bestScore { get; private set; }

    public event Action<int> ScoreChanged;
    public event Action<int> BestScoreChanged;

    void Awake()
    {
        currentScore = 0;
        bestScore = PlayerPrefs.GetInt("bestScore");
    }

    public void AddPoints(int amount)
    {
        currentScore += amount;
        UpdateBestScoreIfNeeded();
        ScoreChanged?.Invoke(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        ScoreChanged?.Invoke(currentScore);
    }

    private void UpdateBestScoreIfNeeded()
    {
        if (currentScore <= bestScore) return;
        bestScore = currentScore;
        PlayerPrefs.SetInt("bestScore", bestScore);
        BestScoreChanged?.Invoke(bestScore);
    }
}
