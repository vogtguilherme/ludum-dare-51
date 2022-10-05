using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapElement : MonoBehaviour
{
    [SerializeField] GameObject MiniElementPrefab;
    public GameObject MiniElement;
    MapManager MapManager;

    void Awake()
    {
        MapManager = FindObjectOfType<MapManager>();
        MiniElement = Instantiate(MiniElementPrefab, Vector3.zero, Quaternion.identity, MapManager.ElementsContainer);
    }
}
