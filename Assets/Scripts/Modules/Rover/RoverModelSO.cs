using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoverData", menuName = "Model/Create Rover Data")]
public class RoverModelSO : ScriptableObject
{
    public float LightSeconds;
    public float StopDistance;
    public float MovingSpeed;
    public float RotatingSpeed;
    [Space(1)]
    public Vector3 startingPosition;
    public LayerMask ObstructionLayerMask;
    public LayerMask HeightLayerMask;    
}
