using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    private GameController gameController;

    [Header("Score")]
    public int score;

    [Header("Time")]
    public bool countTime = true;
    public float startTime;
    private float time;

    private void Start()
    {
        gameController = GetComponent<GameController>();
        scoreText.text = score.ToString();
        time = startTime;
        timeText.text = $"{time.ToString("F2")}s";
    }
    private void Update()
    {
        if(countTime)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
                countTime = false;
                SoundsScript.ss.PlaySound("endBeeps");
                Finish();
            }
            timeText.text = $"{time.ToString("F2")}s";
            if (time < 3.1f && timeText.text == "3.00s" || timeText.text == "2.00s" || timeText.text == "1.00s")
            {
                SoundsScript.ss.PlaySound("smallBeep");
            }
        }
    }
    private void Finish()
    {
        endScoreText.text = $"Score     <b>{score}";

        if (PlayerPrefs.HasKey("bestScore"))
        {
            if(PlayerPrefs.GetInt("bestScore") < score)
            {
                PlayerPrefs.SetInt("bestScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", score);
        }

        StartCoroutine(gameController.End());
    }
    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
