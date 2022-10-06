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
            Vector3 newPosition = Vector3.ClampMagnitude(Player.transform.InverseTransformPoint(Elements[i].transform.position) * Scale, MapRadius * Scale);
            newPosition = new Vector3(newPosition.x, newPosition.z, newPosition.y); //Swaps Y and Z, to make it work in the UI
            MiniTransform.localPosition = newPosition;

            //Scales the icons if they are out of the map reach
            if (MiniTransform.localPosition.magnitude < MapRadius * Scale * 0.999f)
                MiniTransform.localScale = Vector3.one;
            else
                MiniTransform.localScale = Vector3.one * 0.6f;
        }
    }
}
