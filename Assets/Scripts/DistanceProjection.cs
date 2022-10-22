using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceProjection : MonoBehaviour
{
    Camera CameraRover;
    [SerializeField] Transform Player;
    [SerializeField] RectTransform Photo;
    [SerializeField] DistanceMarker[] Markers;

    void Start()
    {
        CameraRover = Camera.main;    
    }
    
    void Update()
    {
        ProjectDistances();
    }

    public void ProjectDistances()
    {
        for (int i = 0; i < Markers.Length; i++)
        {
            RaycastHit hit;
            Vector3 hitPosition = Player.position + (Player.forward * Markers[i].Distance) + (Vector3.up * 100);

            if (Physics.Raycast(hitPosition, Vector3.down, out hit))
            {
                //Puts the markers on the correct position.
                Vector3 screenPosition = CameraRover.WorldToScreenPoint(hit.point);
                Vector3 screenPositionScaled = screenPosition * Photo.rect.height / CameraRover.pixelHeight;
                Vector3 screenPositionOffseted = screenPositionScaled - new Vector3(Photo.rect.width / 2, Photo.rect.height / 2, 0);
                Markers[i].MarkerTransform.localPosition = screenPositionOffseted;

                //Deactivates the markers if they are ocluded by something.
                Vector3 rayDirection = Vector3.Normalize(hit.point - CameraRover.transform.position);
                float distanceFromHit = Vector3.Magnitude(hit.point - CameraRover.transform.position);
                bool IsObstructed = Physics.Raycast(CameraRover.transform.position, rayDirection, distanceFromHit - 0.05f);
                Markers[i].MarkerTransform.gameObject.SetActive(!IsObstructed);            
            }
        }      
    }
}

[System.Serializable]
public class DistanceMarker
{
    public Transform MarkerTransform;
    public float Distance;

}
