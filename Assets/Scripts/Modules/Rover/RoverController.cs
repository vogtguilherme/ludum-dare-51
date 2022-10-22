using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_RoverPrefab = null;
    
    private CommandListener commandListener = null;
    private RoverModel m_RoverModel = null;

    private void InitializeRover()
    {
        if (m_RoverModel == null)
        {
            var _rover = Instantiate(m_RoverPrefab);
            m_RoverModel = _rover.GetComponent<RoverModel>();
        }
    }
}
