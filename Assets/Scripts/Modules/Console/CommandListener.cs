using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandListener : MonoBehaviour, ICommandListener
{
    private CommandQueue m_CommandQueue = null;
    private Command m_CurrentCommand = null;
    
    public void OnSenderUpdate(ICommandSender sender)
    {
        var queue = sender as CommandQueue;
        m_CommandQueue = queue;
    }
}