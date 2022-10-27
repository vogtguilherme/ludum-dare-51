using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraImageViewBind : MonoBehaviour
{
    private Image m_Image = null;

    //ALTERAR ESSE ACESSO
    [SerializeField]
    private RoverCamera roverCamera = null;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (roverCamera != null)
        {
            roverCamera.OnCameraShotTaken += HandleCameraShot;
        }
    }

    private void OnDisable()
    {
        if (roverCamera != null)
        {
            roverCamera.OnCameraShotTaken -= HandleCameraShot;
        }        
    }

    private void HandleCameraShot(Sprite sprite)
    {
        if (m_Image != null)
        {
            m_Image.sprite = sprite;
        }
    }
}
