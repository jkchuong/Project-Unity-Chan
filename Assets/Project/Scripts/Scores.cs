using System;
using System.Collections;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        scoresText.text = "";
        foreach (int score in scores.playerScore)
        {
            scoresText.text += score + ", ";
        }
    }
}
