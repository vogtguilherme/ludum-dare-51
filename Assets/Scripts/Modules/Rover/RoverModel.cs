using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverModel : MonoBehaviour
{
    Camera m_RoverCamera;

    [Header("Model Data")]
    [SerializeField]
    private RoverModelSO m_RoverData = null;

    private float lightSeconds;     //Delay da viagem do comando
    private float stopDistance;
    private float movingSpeed;
    private float rotatingSpeed;

    LayerMask heightLayerMask;
    LayerMask obstructionLayerMask;
    [Space(1)]
    [Header("Testing Only")]
    [SerializeField] float Distance;
    [SerializeField] float Angle;    

    public void Setup()
    {
        m_RoverCamera = Camera.main;

        if (m_RoverData == null)
        {
            lightSeconds = m_RoverData.LightSeconds;
            stopDistance = m_RoverData.StopDistance;
            movingSpeed = m_RoverData.MovingSpeed;
            rotatingSpeed = m_RoverData.RotatingSpeed;
            heightLayerMask = m_RoverData.HeightLayerMask;
            obstructionLayerMask = m_RoverData.ObstructionLayerMask;

            transform.position = m_RoverData.startingPosition;
        }
    }

    [ContextMenu("Start Move")]
    void StartMove()
    {
        StartCoroutine(Move(Distance, movingSpeed));
    }

    [ContextMenu("Start Rotation")]
    void StartRotation()
    {
        StartCoroutine(Rotate(Angle, rotatingSpeed));
    }

    IEnumerator Move(float distance, float speed)
    {
        Debug.Log("Message Sent");
        yield return new WaitForSeconds(lightSeconds);

        float totalDistanceMoved = 0;
        while (totalDistanceMoved < Mathf.Abs(distance))
        {
            //Moving in X and Z axis
            float distanceToMoved = speed * Mathf.Sign(distance) * Time.deltaTime;
            transform.Translate(transform.forward * distanceToMoved, Space.World);
            totalDistanceMoved += Mathf.Abs(distanceToMoved);

            //Moving in the Y axis
            RaycastHit downHit;
            if (Physics.Raycast(transform.position + new Vector3(0, 100, 0), Vector3.down, out downHit, 500, heightLayerMask))
                transform.position = new Vector3(transform.position.x, downHit.point.y, transform.position.z);

            //Checking for obstructions
            if (distance > 0 && Physics.Raycast(m_RoverCamera.transform.position, m_RoverCamera.transform.forward, stopDistance, obstructionLayerMask) || //If the rover is moving forward.
                distance < 0 && Physics.Raycast(m_RoverCamera.transform.position, -m_RoverCamera.transform.forward, stopDistance, obstructionLayerMask)) //If the rover is moving backwards.
                break;

            yield return null;
        }

        yield return new WaitForSeconds(lightSeconds);
        Debug.Log("Message Received");
    }

    IEnumerator Rotate(float angle, float speed)
    {
        Debug.Log("Message Sent");
        yield return new WaitForSeconds(lightSeconds);

        float totalAngleRotated = 0;
        while (totalAngleRotated < Mathf.Abs(angle))
        {
            float angleToRotate = speed * Mathf.Sign(angle) * Time.deltaTime;
            Debug.Log(angleToRotate);
            transform.Rotate(Vector3.up, angleToRotate, Space.World);
            totalAngleRotated += Mathf.Abs(angleToRotate);

            yield return null;
        }

        yield return new WaitForSeconds(lightSeconds);
        Debug.Log("Message Received");
    }
}