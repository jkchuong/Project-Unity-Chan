using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// Should've used a generic canvas script
public class CreditsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel, creditsPanel, backButton;
    
    private GameObject[] canvasObjects => new[] { titlePanel, creditsPanel, backButton };

    private void Start()
    {
        foreach (GameObject canvasObject in canvasObjects)
        {
            canvasObject.transform.localScale = Vector3.zero;
        }
    }
    
    public void DisplayCreditsCanvas()
    {
        foreach (GameObject canvasObject in canvasObjects)
        {
            canvasObject.transform.DOScale(Vector3.one, 0.3f)
                .SetEase(Ease.InBounce);
        }
    }
    
    public void HideCreditsCanvas()
    {
        foreach (GameObject canvasObject in canvasObjects)
        {
            canvasObject.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBounce);
        }
    }
}
