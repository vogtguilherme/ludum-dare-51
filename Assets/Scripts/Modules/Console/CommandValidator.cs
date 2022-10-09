using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandValidator
{
    private List<string> _commands = new List<string>();
    
    public CommandValidator(List<string> cmdList)
    {
        for (int i = 0; i < cmdList.Count; i++)
        {
            if (!_commands.Contains(cmdList[i]))
            {
                _commands.Add(cmdList[i]);
            }            
        }
    }

    public bool ValidateCommandAction(string commandAction)
    {
        if (_commands.Contains(commandAction))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}