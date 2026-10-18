using System;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private GameObject standByUI;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private ScoreController scoreController;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    void Start()
    {
        scoreText.text = scoreController.currentScore.ToString();
        bestScoreText.text = "MEJOR: " + scoreController.bestScore.ToString();
    }

    void OnEnable()
    {
        scoreController.BestScoreChanged += UpdateBestScore;
        scoreController.ScoreChanged += UpdateScore;
    }

    void OnDisable()
    {
        scoreController.BestScoreChanged -= UpdateBestScore;
        scoreController.ScoreChanged -= UpdateScore;
    }

    public void ShowStandby()
    {
        standByUI.SetActive(true);
        scoreUI.SetActive(false);
    }
    public void ShowPlaying()
    {
        standByUI.SetActive(false);
        scoreUI.SetActive(true);
    }

    private void UpdateBestScore(int score)
    {
        bestScoreText.text = "MEJOR: " + score.ToString();
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
