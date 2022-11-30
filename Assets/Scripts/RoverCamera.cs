using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverCamera : MonoBehaviour
{
    [SerializeField] 
    private bool realTimeRender = false;
    [SerializeField]
    private Camera m_RoverCamera = null;

    int height = 1024;
    int width = 1024;
    int depth = 24;

    public event Action<Sprite> OnCameraShotTaken;

    void Awake()
    {
        m_RoverCamera = Camera.main;

        if (m_RoverCamera != null)
        {
            m_RoverCamera.enabled = false;
        }
    }

    public void Initialize()
    {
        TakeShot();
    }

    [ContextMenu("Take Shot")]
    public void TakeShot()
    {
        if (m_RoverCamera != null)
        {
            m_RoverCamera.Render();
        }
        else
        {
            Debug.LogWarning("Camera is not set up");
        }
    }

    [ContextMenu("Set Real Time")]
    public void SetRealTime()
    {
        m_RoverCamera.enabled = realTimeRender;
    }

    public void CaptureImage()
    {
        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        Rect rect = new Rect(0, 0, width, height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        m_RoverCamera.targetTexture = renderTexture;
        m_RoverCamera.Render();

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        m_RoverCamera.targetTexture = null;
        RenderTexture.active = currentRenderTexture;
        Destroy(renderTexture);

        Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);

        OnCameraShotTaken?.Invoke(sprite);
    }
}
