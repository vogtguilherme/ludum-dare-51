using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceProjection : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Camera CameraRover;
    [SerializeField] Transform Marker;
    [SerializeField] RectTransform Photo;
    [SerializeField] float Distance;

    void Start()
    {
        
    }

    void Update()
    {
        ProjectDistances();
    }

    public void ProjectDistances()
    {
        RaycastHit hit;
        Vector3 hitPosition = Player.position + (Player.forward * Distance) + (transform.up * 100);

        if (Physics.Raycast(hitPosition, Vector3.down, out hit))
        {
            Vector3 screenPosition = CameraRover.WorldToScreenPoint(hit.point);
            Vector3 screenPositionScaled = screenPosition * Photo.rect.height / CameraRover.pixelHeight;
            Vector3 screenPositionOffseted = screenPositionScaled - new Vector3(Photo.rect.width / 2, Photo.rect.height / 2, 0);
            Marker.localPosition = screenPositionOffseted;
            
            Debug.Log(hit.transform.name + ", " + hitPosition + ", " + screenPosition + ", " + screenPositionScaled + ", " + screenPositionOffseted);
        }
    }
}
