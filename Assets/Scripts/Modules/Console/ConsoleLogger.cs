using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleLogger : MonoBehaviour
{
    public event Action<LogType> OnMessageLogged;
    
    [SerializeField]
    private TMP_Text m_ConsoleText = null;

    private List<string> m_ConsoleOutputList = new List<string>();
    
    public void LogMessage(string message, LogType logType)
    {
        if (!string.IsNullOrEmpty(message))
        {
            _LogMessage(message, logType);
        }       
    }

    private void _LogMessage(string message, LogType logType)
    {
        if (m_ConsoleText == null)
        {
            Debug.LogWarning("Please assign a TMP_Text component to the Console Text reference");
            return;
        }
        
        m_ConsoleOutputList.Add(message);

        m_ConsoleText.text += message + "\n";
        
        OnMessageLogged?.Invoke(logType);
    }
}

public enum LogType
{
    Message, Warning, Error, Info
}