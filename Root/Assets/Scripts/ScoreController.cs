using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score = 0;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        scoreText.text = "Score : " + score;
    }

    public void UpdateScore(int increment)
    {
        score += increment;
        RefreshUI();
    }
}
