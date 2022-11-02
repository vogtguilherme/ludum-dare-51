using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] MapElement[] Elements;
    private float Scale = 10.2f;
    private float MapRadius = 14;
    public Transform ElementsContainer;

    void Start()
    {
        Elements = FindObjectsOfType<MapElement>();
        UpdateMap();
    }

    public void UpdateMap()
    {
        for (int i = 0; i < Elements.Length; i++)
        {
            Transform MiniTransform = Elements[i].MiniElement.transform; //Just to make it shorter.
            Vector3 newPosition = Vector3.ClampMagnitude(Player.transform.InverseTransformPoint(Elements[i].transform.position) * Scale, MapRadius * Scale); //Transforms the positions of the elements, using the local position and rotation of the player as reference. Plus, scales and clamps the values, to fit in the map. relative to
            newPosition = new Vector3(newPosition.x, newPosition.z, newPosition.y); //Swaps Y and Z, to make it work in the UI.
            MiniTransform.localPosition = newPosition; //Just assigns the new position to the transform of the element.

            //Scales the icons if they are out of the map reach
            if (MiniTransform.localPosition.magnitude < MapRadius * Scale * 0.999f)
                MiniTransform.localScale = Vector3.one;
            else
                MiniTransform.localScale = Vector3.one * 0.6f;
        }
    }
}
