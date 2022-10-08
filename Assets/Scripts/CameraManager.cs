using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] bool RealTime;
    [SerializeField] Camera CameraRover;

    void Awake()
    {
        if (!RealTime)
            CameraRover.enabled = false;
    }

    void Start()
    {
        TakeShot();

    }

    [ContextMenu("Take Shot")]
    public void TakeShot()
    {
        CameraRover.Render();
    }

    [ContextMenu("Set Real Time")]
    public void SetRealTime()
    {
        CameraRover.enabled = RealTime;
    }
}
