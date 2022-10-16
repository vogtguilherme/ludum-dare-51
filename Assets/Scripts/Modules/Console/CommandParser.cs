using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParser : MonoBehaviour
{
    [SerializeField]
    private InputReader m_InputReader = null;
    [Header("Command Prompter Properties")]
    private string[] m_Command;

    public event Action<string[]> OnCommandParsed;

    private void Start()
    {
        if(m_InputReader != null)
        {
            m_InputReader.OnEndEdit += ParseCommandInput;
        }
    }

    private void ParseCommandInput(string command)
    {
        string[] args = command.Split(' ');
        m_Command = args;
        if (m_Command != null || m_Command.Length > 0)
        {
            OnCommandParsed?.Invoke(m_Command);
        }        
    }
}