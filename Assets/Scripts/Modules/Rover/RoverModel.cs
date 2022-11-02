using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class RoverModel : MonoBehaviour
{
    Camera m_MainCamera;
    [SerializeField]
    RoverCamera m_RoverCamera = null;
    [SerializeField]
    MapManager m_MapManager = null;

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

    private void Awake()
    {
        m_RoverCamera = GetComponent<RoverCamera>();
        m_MapManager = FindObjectOfType<MapManager>();
    }

    public void Setup()
    {
        m_MainCamera = Camera.main;

        if (m_RoverData != null)
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
        StartCoroutine(Move(Distance));
    }

    [ContextMenu("Start Rotation")]
    void StartRotation()
    {
        StartCoroutine(Rotate(Angle));
    }

    public Command GetCommandLogic(Command command)
    {
        float parameter = 0;        
        if (command.instructions.Length > 1)
        {
            parameter = float.Parse(command.instructions[1]);
        }

        switch (command.key)
        {
            case "move":
                command.OnExecute = Move(parameter);
                break;
            case "rotate":
                command.OnExecute = Rotate(parameter);
                break;
            case "recon":
                command.OnExecute = Recon();
                break;
        }

        return command;
    }

    IEnumerator Move(float distance)
    {
        Debug.Log("Message Sent");
        yield return new WaitForSeconds(lightSeconds);

        float totalDistanceMoved = 0;
        while (totalDistanceMoved < Mathf.Abs(distance))
        {
            //Moving in X and Z axis
            float distanceToMoved = movingSpeed * Mathf.Sign(distance) * Time.deltaTime;
            transform.Translate(transform.forward * distanceToMoved, Space.World);
            totalDistanceMoved += Mathf.Abs(distanceToMoved);

            //Moving in the Y axis
            RaycastHit downHit;
            if (Physics.Raycast(transform.position + new Vector3(0, 100, 0), Vector3.down, out downHit, 500, heightLayerMask))
                transform.position = new Vector3(transform.position.x, downHit.point.y, transform.position.z);

            //Checking for obstructions
            if (distance > 0 && Physics.Raycast(m_MainCamera.transform.position, m_MainCamera.transform.forward, stopDistance, obstructionLayerMask) || //If the rover is moving forward.
                distance < 0 && Physics.Raycast(m_MainCamera.transform.position, -m_MainCamera.transform.forward, stopDistance, obstructionLayerMask)) //If the rover is moving backwards.
                break;

            yield return null;
        }

        yield return new WaitForSeconds(lightSeconds);
        Debug.Log("Message Received");
    }

    IEnumerator Rotate(float angle)
    {
        Debug.Log("Message Sent");
        yield return new WaitForSeconds(lightSeconds);

        float totalAngleRotated = 0;
        while (totalAngleRotated < Mathf.Abs(angle))
        {
            float angleToRotate = rotatingSpeed * Mathf.Sign(angle) * Time.deltaTime;
            transform.Rotate(Vector3.up, angleToRotate, Space.World);
            totalAngleRotated += Mathf.Abs(angleToRotate);

            yield return null;
        }

        yield return new WaitForSeconds(lightSeconds);
        Debug.Log("Message Received");
    }

    IEnumerator Recon()
    {
        yield return new WaitForSeconds(lightSeconds);

        m_RoverCamera.CaptureImage();
        m_MapManager.UpdateMap();

        Debug.Log("Click!");
    }
}