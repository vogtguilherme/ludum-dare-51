using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandListener : ICommandListener
{
    public event Action<Command> OnUpdateListener;
    
    private Command m_CurrentCommand = null;
    
    public void UpdateListener(Command command)
    {
        m_CurrentCommand = command;
        OnUpdateListener?.Invoke(m_CurrentCommand);
    }
}