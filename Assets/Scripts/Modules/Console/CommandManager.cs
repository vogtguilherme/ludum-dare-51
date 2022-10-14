using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField]
    private ConsoleLogger m_ConsoleLogger = null;
    [SerializeField]
    private CommandParser m_CommandParser = null;
    [SerializeField]
    private List<string> m_DefaultCommandList = new List<string>();

    private CommandQueue m_CurrentQueue;

    private CommandValidator m_Validator = null;

    public CommandValidator Validator
    {
        get { return m_Validator; }
    }

    private void Awake()
    {
        m_ConsoleLogger = GetComponent<ConsoleLogger>();
        m_CurrentQueue = new CommandQueue(this);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_Validator = new CommandValidator(m_DefaultCommandList);
        m_CommandParser.OnCommandParsed += OnCommandParsed;
    }

    private void OnCommandParsed(string[] command)
    {
        if (m_Validator != null)
        {
            if (m_Validator.ValidateCommandAction(command[0]))
            {
                //Debug.Log(command[0] + " " + command[1]);
                string _command = string.Empty;
                for (int i = 0; i < command.Length; i++)
                {
                    if (i == 0)
                    {
                        _command = command[0];
                    }
                    else
                    {
                        _command = _command + " " + command[i];
                    }
                }

                m_CurrentQueue.AddCommand(_command, HandleCommandAdded);
            }
            else
            {
                Debug.Log("command not found");
                string commandNotFound = "command not found: " + command[0];
                m_ConsoleLogger.LogMessage(commandNotFound, LogType.Error);
            }
        }
    }

    private void HandleCommandAdded(string command)
    {
        string message = "queued command: " + command;
        m_ConsoleLogger.LogMessage(message, LogType.Info);
    }
}
