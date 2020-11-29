using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
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
                Finish();
            }
            timeText.text = $"{time.ToString("F2")}s";
        }
    }
    private void Finish()
    {
        gameController.End();
    }
    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
