using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_RoverPrefab = null;
    [SerializeField]
    private RoverModel m_RoverModel = null;

    private CommandListener commandListener = null;    

    #region Monobehavior Callbacks

    private void Awake()
    {
        commandListener = new CommandListener();
    }

    private void Start()
    {
        var _commandManager = CommandManager.Instance;
        if (_commandManager != null)
        {
            _commandManager.AttachListener(commandListener);
        }

        Initialize();
    }

    #endregion

    private void Initialize()
    {
        InitializeRover();
    }

    private void InitializeRover()
    {
        m_RoverModel = GameObject.FindObjectOfType<RoverModel>();
        if (m_RoverModel == null)
        {
            var _rover = Instantiate(m_RoverPrefab);
            m_RoverModel = _rover.GetComponent<RoverModel>();
        }

        m_RoverModel.Setup();
    }    
}