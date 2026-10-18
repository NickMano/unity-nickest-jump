using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private GameObject standByUI;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private ScoreController scoreController;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    [SerializeField] private Text standbyText;

    private readonly LocalizedString tapToPlay =
        new LocalizedString("UI", "tap_to_play");

    private readonly LocalizedString bestScore =
        new LocalizedString("UI", "best_score");

    private readonly IntVariable scoreVariable = new IntVariable();

    void Start()
    {
        scoreText.text = scoreController.currentScore.ToString();
    }

    void OnEnable()
    {
        scoreVariable.Value = scoreController.bestScore;
        bestScore["score"] = scoreVariable;

        tapToPlay.StringChanged += UpdateStandbyText;
        bestScore.StringChanged += UpdateBestScoreText;

        scoreController.BestScoreChanged += UpdateBestScore;
        scoreController.ScoreChanged += UpdateScore;
    }

    void OnDisable()
    {
        tapToPlay.StringChanged -= UpdateStandbyText;
        bestScore.StringChanged -= UpdateBestScoreText;

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
        scoreVariable.Value = score;
    }

    private void UpdateStandbyText(string value)
    {
        standbyText.text = value;
    }

    private void UpdateBestScoreText(string value)
    {
        bestScoreText.text = value;
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
