using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static event Action<int> ScoreValueChanged;

    private static int scoreUseProperty;

    public static int scoreCount
    {
        get => scoreUseProperty;
        private set
        {
            scoreUseProperty = value;
            ScoreValueChanged?.Invoke(scoreUseProperty);
        }
    }

    public void CollectScore()
    {
        scoreCount++;
    }

}
