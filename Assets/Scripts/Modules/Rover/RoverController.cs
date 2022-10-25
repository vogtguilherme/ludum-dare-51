using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandQueue))]
public class RoverController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_RoverPrefab = null;

    private RoverModel m_RoverModel = null;
    private CommandListener m_CommandListener = null;    
    private CommandQueue m_CommandQueue = null;

    #region Monobehavior Callbacks

    private void Awake()
    {
        m_CommandListener = new CommandListener();
        m_CommandQueue = GetComponent<CommandQueue>();
    }

    private void Start()
    {
        var _commandManager = CommandManager.Instance;
        if (_commandManager != null)
        {
            _commandManager.AttachListener(m_CommandListener);
        }

        Initialize();
    }

    #endregion

    private void Initialize()
    {
        InitializeRover();
        m_CommandListener.OnUpdateListener += ProcessReceivedCommand;
    }

    private void OnDestroy()
    {
        m_CommandListener.OnUpdateListener -= ProcessReceivedCommand;
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

    private void ProcessReceivedCommand(Command receivedCommand)
    {
        if (m_CommandQueue != null)
        {
            var _logic = m_RoverModel.GetCommandLogic(receivedCommand);
            m_CommandQueue.Enqueue(_logic);
            CommandManager.Instance.LogMessage("Sending command instruction", LogType.Info);
        }
    }
}