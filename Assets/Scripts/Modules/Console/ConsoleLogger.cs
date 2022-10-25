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

        string richTextTag = LogContextColor(logType);
        m_ConsoleText.text += richTextTag + message + "\n";
        
        OnMessageLogged?.Invoke(logType);
    }

    private string LogContextColor(LogType type)
    {
        string _colorTag = string.Empty;
        
        switch (type)
        {
            case LogType.Message:
                _colorTag = "<color=#FFFFFF>";  //White
                break;
            case LogType.Warning:
                _colorTag = "<color=#FFCE00>";  //Yellow
                break;
            case LogType.Error:
                _colorTag = "<color=#CF2217>";  //Red
                break;
            case LogType.Info:
                _colorTag = "<color=#004B00>";  //Green
                break;
            default:
                break;
        }

        return _colorTag;
    }
}

public enum LogType
{
    Message, Warning, Error, Info
}