using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandService : MonoBehaviour
{
    [SerializeField]private List<Command> m_DefaultCommands = new List<Command>();

    private Dictionary<string, Command> m_CommandKeyValuePairs = new Dictionary<string, Command>();

    private CommandValidator m_Validator;

    private void Setup()
    {
        if (m_DefaultCommands.Count > 0)
        {
            foreach (Command cmd in m_DefaultCommands)
            {
                m_CommandKeyValuePairs.Add(cmd.key, cmd);
            }

            m_Validator = new CommandValidator(m_DefaultCommands);
        }
    }

    public CommandRequest RequestCommand(string[] commandEntry)
    {
        CommandRequest request = new CommandRequest(commandEntry);
        var validation = m_Validator.ValidateCommand(request.commandKeys);

        if (!validation.validated)
        {
            request.ExceptionHandler = validation.validationLog;
        }
        else
        {
            var _command = GetCommand(commandEntry);
            request.SetCommand(_command);
        }

        return request;
    }

    private Command GetCommand(string[] entry)
    {
        Command command = null;
        return command;
    }
}

public class CommandRequest
{
    public Command Result { get; private set; }

    public string ExceptionHandler = null;

    public string[] commandKeys;
    
    public CommandRequest(string[] commandKeys)
    {
        this.commandKeys = commandKeys;
    }

    public void SetCommand(Command command)
    {
        Result = command;
    }
}