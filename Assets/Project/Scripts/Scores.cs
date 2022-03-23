using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField] private ScoresScriptableObject scores;

    private TextMeshProUGUI scoresText;

    private void Awake()
    {
        scoresText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScores()
    {
        IEnumerable<int> sortedScores = scores.playerScore.OrderByDescending(i => i).Take(5);
        scoresText.text = "";
        foreach (int score in sortedScores)
        {
            scoresText.text += score + "\n";
        }
    }
}
