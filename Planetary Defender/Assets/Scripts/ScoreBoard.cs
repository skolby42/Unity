using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scorePerSecond = 5;
    [SerializeField] float timeScoreStart = 3f;
    int score;
    Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScore();

        InvokeRepeating(nameof(AddTimeScore), timeScoreStart, 1f);
    }

    public void ScoreHit(int scorePerHit = 10)
    {
        UpdateScore(scorePerHit);
    }

    private void AddTimeScore()
    {
        UpdateScore(scorePerSecond);
    }

    private void UpdateScore(int scoreIncrement = 0)
    {
        score += scoreIncrement;
        scoreText.text = score.ToString();
    }
}
