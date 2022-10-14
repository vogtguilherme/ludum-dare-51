using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandQueue
{
    [SerializeField]
    private List<string> m_Commands = new List<string>();

    private CommandManager m_Manager = null;
    private int m_QueueIndex = 0;

    public List<string> Commands
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

    public void AddCommand(string command, Action<string> OnCommandAdded = null)
    {
        m_Commands.Add(command);
        OnCommandAdded?.Invoke(command);
    }
}
