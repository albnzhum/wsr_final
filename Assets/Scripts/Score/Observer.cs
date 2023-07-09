using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private int scoreRequiredToWin;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Canvas winCanvas;

    private void OnScoreValueChanged(int score)
    {
        Print(score);
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    private void Print(int score)
    {
        if (CheckForWin(score))
        {
            winCanvas.gameObject.SetActive(true);
            StartCoroutine(delayAnimator(2f));
        }
        else
        {
            int scoreNeeded = scoreRequiredToWin - score;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private IEnumerator delayAnimator(float delay)
    {
        yield return new WaitForSeconds(delay);
        winCanvas.gameObject.SetActive(false);
    }

    private bool CheckForWin(int score)
    {
        bool hasWon = score == scoreRequiredToWin;
        return hasWon;
    }

    private void OnEnable()
    {
        Score.ScoreValueChanged += OnScoreValueChanged;
    }

    private void OnDisable()
    {
        Score.ScoreValueChanged -= OnScoreValueChanged;
    }
}
