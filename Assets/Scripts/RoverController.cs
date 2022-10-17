using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    Camera RoverCamera;
    [SerializeField] float LightSeconds;
    [SerializeField] float StopDistance;
    [SerializeField] float MovingSpeed;
    [SerializeField] float RotatingSpeed;
    [SerializeField] LayerMask HeightLayerMask;
    [SerializeField] LayerMask ObstructionLayerMask;
    [Space]
    [SerializeField] float Distance;
    [SerializeField] float Angle;

    void Start()
    {
        RoverCamera = Camera.main;    
    }

    [ContextMenu("Start Move")]
    void StartMove()
    {
        StartCoroutine(Move(Distance, MovingSpeed));
    }

    [ContextMenu("Start Rotation")]
    void StartRotation()
    {
        StartCoroutine(Rotate(Angle, RotatingSpeed));
    }

    IEnumerator Move(float distance, float speed)
    {
        Debug.Log("Message Sent");
        yield return new WaitForSeconds(LightSeconds);

        float totalDistanceMoved = 0;
        while (totalDistanceMoved < Mathf.Abs(distance))
        {
            //Moving in X and Z axis
            float distanceToMoved = speed * Mathf.Sign(distance) * Time.deltaTime;
            transform.Translate(transform.forward * distanceToMoved, Space.World);
            totalDistanceMoved += Mathf.Abs(distanceToMoved);

            //Moving in the Y axis
            RaycastHit downHit;
            if (Physics.Raycast(transform.position + new Vector3(0, 100, 0), Vector3.down, out downHit, 500, HeightLayerMask))
                transform.position = new Vector3(transform.position.x, downHit.point.y, transform.position.z);

            //Checking for obstructions
            if (distance > 0 && Physics.Raycast(RoverCamera.transform.position, RoverCamera.transform.forward, StopDistance, ObstructionLayerMask) || //If the rover is moving forward.
                distance < 0 && Physics.Raycast(RoverCamera.transform.position, - RoverCamera.transform.forward, StopDistance, ObstructionLayerMask)) //If the rover is moving backwards.
                break;

            yield return null;
        }
        
        yield return new WaitForSeconds(LightSeconds);
        Debug.Log("Message Received");
    }

    IEnumerator Rotate(float angle, float speed)
    {
        Debug.Log("Message Sent");
        yield return new WaitForSeconds(LightSeconds);

        float totalAngleRotated = 0;
        while (totalAngleRotated < Mathf.Abs(angle))
        {
            float angleToRotate = speed * Mathf.Sign(angle) * Time.deltaTime;
            Debug.Log(angleToRotate);
            transform.Rotate(Vector3.up, angleToRotate, Space.World);
            totalAngleRotated += Mathf.Abs(angleToRotate);
            
            yield return null;
        }

        yield return new WaitForSeconds(LightSeconds);
        Debug.Log("Message Received");
    }
}
