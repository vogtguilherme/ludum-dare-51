using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandService : MonoBehaviour
{
    private CommandManager m_Manager;
    
    [SerializeField]private List<CommandSO> m_DefaultCommands = new List<CommandSO>();

    private Dictionary<string, Command> m_CommandKeyValuePairs = new Dictionary<string, Command>();

    private CommandValidator m_Validator;

    public void Setup(CommandManager manager)
    {
        m_Manager = manager;
        
        if (m_DefaultCommands.Count > 0)
        {            
            var _commandList = new List<Command>();
            foreach (CommandSO cmd in m_DefaultCommands)
            {
                cmd.CreateCommand(manager);                
                m_CommandKeyValuePairs.Add(cmd.commandKey, cmd.commandObj);
                _commandList.Add(cmd.commandObj);
            }

            m_Validator = new CommandValidator(_commandList);
        }
    }

    public CommandRequest RequestCommand(string[] commandEntry)
    {
        CommandRequest _request = new CommandRequest(commandEntry);
        var _validation = m_Validator.ValidateCommand(_request.commandKeys);

        if (!_validation.validated)
        {
            _request.ExceptionHandler = _validation.validationLog;
        }
        else
        {
            var _command = GetCommand(commandEntry);
            _request.SetCommand(_command);
        }

        return _request;
    }

    private Command GetCommand(string[] entry)
    {
        Command _command = null;

        _command = m_CommandKeyValuePairs[entry[0]];

        return _command;
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