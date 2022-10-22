using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour, ICommandSender
{
    public event Action OnManagerInitialized;
    
    [Header("Command Settings")]
    [SerializeField]
    private CommandService m_CommandService = null;
    [SerializeField]
    private CommandParser m_CommandParser = null;
    [Space(1)]
    [Header("Console Settings")]
    [SerializeField]
    private ConsoleLogger m_ConsoleLogger = null;

    private CommandQueue m_CurrentQueue;

    public CommandQueue CurrentQueue
    {
        get
        {
            return m_CurrentQueue;
        }
    }

    private void Awake()
    {
        m_CommandService = GetComponent<CommandService>();
        m_ConsoleLogger = GetComponent<ConsoleLogger>();
        m_CurrentQueue = new CommandQueue(this);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_CommandParser.OnCommandParsed += OnCommandParsed;
        m_CommandService.Setup(this);

        OnManagerInitialized?.Invoke();
    }

    private void OnCommandParsed(string[] command)
    {
        var _request = m_CommandService.RequestCommand(command);
        if (_request.Result != null)
        {
            m_CurrentQueue.AddCommand(_request.Result, HandleCommandAdded);
            CommandAdded(_request.Result);
        }
        else
        {
            m_ConsoleLogger.LogMessage(_request.ExceptionHandler, LogType.Error);
        }
    }

    private void HandleCommandAdded(Command command)
    {
        string message = "queued command: " + command.key;
        m_ConsoleLogger.LogMessage(message, LogType.Info);
    }

    public event Action<Command> OnCommandAdded;

    private void CommandAdded(Command command)
    {
        OnCommandAdded?.Invoke(command);

        NotifyListener();
    }

    private ICommandListener m_Listener;

    public void AttachListener(ICommandListener listener)
    {
        Debug.Log("Attached a linstener.");
        m_Listener = listener;
    }

    public void RemoveListener(ICommandListener listener)
    {
        Debug.Log("Removed a listener.");
        m_Listener = null;
    }

    public void NotifyListener()
    {
        if (m_Listener != null)
        {
            m_Listener.OnSenderUpdate(this);
        }
    }
}
