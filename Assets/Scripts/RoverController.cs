using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    Camera RoverCamera;
    [SerializeField] float LightSeconds;
    [SerializeField] float RoverSpeed;
    [SerializeField] float Distance;
    [SerializeField] float StopDistance;
    [SerializeField] LayerMask HeightLayerMask;
    [SerializeField] LayerMask ObstructionLayerMask;

    void Start()
    {
        RoverCamera = Camera.main;    
    }

    [ContextMenu("Start Move")]
    void StartMove()
    {
        StartCoroutine(Move(Distance, RoverSpeed));
    }

    IEnumerator Move(float distance, float speed)
    {
        Debug.Log("Message Sent");
        yield return new WaitForSeconds(LightSeconds);

        float totalDistanceMoved = 0;
        while (totalDistanceMoved < distance)
        {
            //Moving in X and Z axis
            float distanceToMoved = speed * Time.deltaTime;
            transform.Translate(transform.forward * distanceToMoved, Space.World);
            totalDistanceMoved += distanceToMoved;

            //Moving in the Y axis
            RaycastHit downHit;
            if (Physics.Raycast(transform.position + new Vector3(0, 100, 0), Vector3.down, out downHit, 500, HeightLayerMask))
                transform.position = new Vector3(transform.position.x, downHit.point.y, transform.position.z);

            //Checking for obstructions
            if (Physics.Raycast(RoverCamera.transform.position, RoverCamera.transform.forward, StopDistance, ObstructionLayerMask))
                break;

            yield return null;
        }
        
        yield return new WaitForSeconds(LightSeconds);
        Debug.Log("Message Received");
    }
}
