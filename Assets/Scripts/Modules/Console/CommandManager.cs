using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
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
    }

    private void OnCommandParsed(string[] command)
    {
        
    }

    private void HandleCommandAdded(string command)
    {
        string message = "queued command: " + command;
        m_ConsoleLogger.LogMessage(message, LogType.Info);
    }
}
