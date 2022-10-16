using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandValidator
{
    private List<Command> m_DefaultCommands = new List<Command>();
    
    public CommandValidator(List<Command> cmdList)
    {
        for (int i = 0; i < cmdList.Count; i++)
        {
            if (!m_DefaultCommands.Contains(cmdList[i]))
            {
                m_DefaultCommands.Add(cmdList[i]);
            }            
        }
    }

    public CommandValidation ValidateCommand(string[] command)
    {
        CommandValidation actionValidation = ValidateCommandAction(command[0]);
        return actionValidation;
    }

    private CommandValidation ValidateCommandAction(string commandKey)
    {
        bool _validation = false;
        string _log = string.Empty;
        Debug.Log(commandKey);

        for (int i = 0; i < m_DefaultCommands.Count; i++)
        {
            if (commandKey.Equals(m_DefaultCommands[i].key))
            {
               _validation = true;
            }
        }
        if (_validation == false)
        {
            _log = "command not found: " + commandKey;
        }

        CommandValidation validation = new CommandValidation(commandKey, _log, _validation);

        return validation;
    }
}

public struct CommandValidation
{
    public string command { get; }
    public string validationLog { get; }
    public bool validated { get; }

    public CommandValidation(string command, string validationLog, bool validated)
    {
        this.command = command;
        this.validationLog = validationLog;
        this.validated = validated;
    }

    public void Show()
    {
        Debug.Log(command + " " + validationLog + " " + validated);
    }
}