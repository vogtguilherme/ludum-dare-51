using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandQueue
{
    [SerializeField]
    private List<Command> m_Commands = new List<Command>();

    private CommandManager m_Manager = null;
    private int m_QueueIndex = 0;

    public List<Command> Commands
    {
        get
        {
            return m_Commands;
        }
    }

    public CommandQueue(CommandManager manager)
    {
        m_Manager = manager;
    }

    public void AddCommand(Command command, Action<Command> OnCommandAdded = null)
    {
        m_Commands.Add(command);
        OnCommandAdded?.Invoke(command);
    }
}
