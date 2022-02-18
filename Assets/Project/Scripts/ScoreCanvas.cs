using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Should've used a generic canvas script
public class ScoreCanvas : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel, scoresPanel, backButton;

    private Scores scores;
    private IEnumerable<GameObject> canvasObjects => new[] { titlePanel, scoresPanel, backButton };
    
    private void Start()
    {
        foreach (GameObject canvasObject in canvasObjects)
        {
            canvasObject.transform.localScale = Vector3.zero;
        }

        scores = GetComponentInChildren<Scores>();
    }

    public void DisplayScoreCanvas()
    {
        scores.UpdateScores();

        foreach (GameObject canvasObject in canvasObjects)
        {
            canvasObject.transform.DOScale(Vector3.one, 0.3f)
                .SetEase(Ease.InBounce);
        }
    }
    
    public void HideScoreCanvas()
    {
        foreach (GameObject canvasObject in canvasObjects)
        {
            canvasObject.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBounce);
        }
    }
}
