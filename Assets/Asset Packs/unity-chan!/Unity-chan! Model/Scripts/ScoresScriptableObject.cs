using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scores", menuName = "ScriptableObjects/ScoresScriptableObject")]
public class ScoresScriptableObject : ScriptableObject
{
    public List<string> playerName;
    public List<int> playerScore;
}
