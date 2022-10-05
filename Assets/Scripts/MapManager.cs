using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] MapElement[] Elements;
    [SerializeField] float Scale = 1;
    [SerializeField] float MapRadius = 1;
    public Transform ElementsContainer;

    void Start()
    {
        Elements = FindObjectsOfType<MapElement>();
    }

    void Update()
    {
        for (int i = 0; i < Elements.Length; i++)
        {
            Transform MiniTransform = Elements[i].MiniElement.transform;

            MiniTransform.position = Vector3.ClampMagnitude(Player.transform.InverseTransformPoint(Elements[i].transform.position) * Scale, MapRadius * Scale);

            MiniTransform.position = new Vector3(MiniTransform.position.x, MiniTransform.position.z, MiniTransform.position.y);

            if (MiniTransform.position.magnitude < MapRadius * Scale * 0.999f)
                MiniTransform.localScale = Vector3.one;
            else
                MiniTransform.localScale = Vector3.one * 0.4f;
        }
    }
}
