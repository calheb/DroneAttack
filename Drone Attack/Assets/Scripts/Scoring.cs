using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoring : MonoBehaviour
{
    public static int CurrentScore = 0;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Score: " + CurrentScore;
    }
}