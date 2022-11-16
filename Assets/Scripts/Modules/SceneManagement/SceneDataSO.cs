using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scene/New Scene Data")]
public class SceneDataSO : ScriptableObject
{
    public SceneAsset sceneAsset;
}