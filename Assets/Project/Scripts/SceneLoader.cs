using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingCamera;

    private const string LOCOMOTION_SCENE = "Locomotion";
    
    private int NumberOfScenes => SceneManager.sceneCountInBuildSettings;

    private void Start()
    {
        for (int sceneBuildIndex = 0; sceneBuildIndex < NumberOfScenes; sceneBuildIndex++)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
                if (scene.name == LOCOMOTION_SCENE)
                {
                    SceneManager.SetActiveScene(scene);
                }
            }
        }
        
        loadingCamera.SetActive(false);
    }
}
