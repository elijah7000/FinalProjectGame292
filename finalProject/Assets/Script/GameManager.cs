using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Music and Gameplay Control
    public AudioSource backgroundMusic;
    public BeatController beatControl;
    public bool isGameStarted;

    // Scoring Variables
    public int currentScore;
    public int scoreForNormalHit = 100;
    public int scoreForGoodHit = 125;
    public int scoreForPerfectHit = 150;

    // Multiplier Variables
    public int currentMultiplier = 1;
    private int multiplierProgress;
    public int[] multiplierThresholds;

    // UI Elements
    public Text scoreText;
    public Text multiplierText;

    // Hit Tracking
    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    // Results Screen
    public GameObject resultsScreen;
    public Text percentHitText;
    public Text normalHitsText;
    public Text goodHitsText;
    public Text perfectHitsText;
    public Text missedHitsText;
    public Text rankText;
    public Text finalScoreText;

    private void Start()
    {
        Instance = this;

        ResetGame();
        totalNotes = FindObjectsOfType<NoteHandler>().Length;
    }

    private void Update()
    {
        if (!isGameStarted)
        {
            CheckForGameStart();
        }
        else if (!backgroundMusic.isPlaying && !resultsScreen.activeInHierarchy)
        {
            ShowResults();
        }
    }

    private void CheckForGameStart()
    {
        if (Input.anyKeyDown)
        {
            isGameStarted = true;
            beatControl.hasStarted = true;
            backgroundMusic.Play();
        }
    }

    private void ResetGame()
    {
        scoreText.text = "Score: 0";
        multiplierText.text = "Multiplier: x1";
        currentScore = 0;
        currentMultiplier = 1;
        multiplierProgress = 0;
    }

    private void ShowResults()
    {
        resultsScreen.SetActive(true);

        normalHitsText.text = normalHits.ToString();
        goodHitsText.text = goodHits.ToString();
        perfectHitsText.text = perfectHits.ToString();
        missedHitsText.text = missedHits.ToString();

        float totalHits = normalHits + goodHits + perfectHits;
        float hitPercentage = (totalHits / totalNotes) * 100f;

        percentHitText.text = $"{hitPercentage:F1}%";
        rankText.text = CalculateRank(hitPercentage);
        finalScoreText.text = currentScore.ToString();
    }

    private string CalculateRank(float percentage)
    {
        if (percentage > 35) return "A";
        if (percentage > 30) return "B";
        if (percentage > 25) return "C";
        if (percentage > 20) return "D";
        return "F";
    }

    // Note Events
    public void RegisterNormalHit()
    {
        AddScore(scoreForNormalHit);
        normalHits++;
    }

    public void RegisterGoodHit()
    {
        AddScore(scoreForGoodHit);
        goodHits++;
    }

    public void RegisterPerfectHit()
    {
        AddScore(scoreForPerfectHit);
        perfectHits++;
    }

    public void RegisterMissedNote()
    {
        currentMultiplier = 1;
        multiplierProgress = 0;

        UpdateMultiplierText();
        missedHits++;
    }

    private void AddScore(int score)
    {
        currentScore += score * currentMultiplier;
        UpdateMultiplier();
        UpdateScoreText();
    }

    private void UpdateMultiplier()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierProgress++;
            if (multiplierProgress >= multiplierThresholds[currentMultiplier - 1])
            {
                multiplierProgress = 0;
                currentMultiplier++;
            }
        }

        UpdateMultiplierText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {currentScore}";
    }

    private void UpdateMultiplierText()
    {
        multiplierText.text = $"Multiplier: x{currentMultiplier}";
    }
}
