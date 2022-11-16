using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public SceneDataSO firstScene = null;

    private void Start()
    {
        SceneController.Instance.RequestSceneLoad(firstScene, null, true);
    }
}
