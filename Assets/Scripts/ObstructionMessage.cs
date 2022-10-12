using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class ObstructionMessage : MonoBehaviour
{
    [SerializeField] Camera CameraRover;
    [SerializeField] GameObject UIMessage;
    [SerializeField] TMP_Text TextObstruction;
    [SerializeField] TMP_Text TextDistance;
    [SerializeField] float CheckDistance;
    [SerializeField] LayerMask LayersToCheck;


    void Update()
    {
        CheckObstruction();
    }

    void CheckObstruction()
    {
        bool isObstructed;
        
        RaycastHit hit;
        Ray ray = new Ray(CameraRover.transform.position, CameraRover.transform.forward);

        isObstructed = Physics.Raycast(ray, out hit, CheckDistance, LayersToCheck);

        UIMessage.SetActive(isObstructed);

        if (isObstructed)
        {
            float distanceRounded = hit.distance; //Mathf.Round(10 * hit.distance) / 10;
            TextObstruction.text = hit.transform.tag.ToUpper();
            TextDistance.text = distanceRounded.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
