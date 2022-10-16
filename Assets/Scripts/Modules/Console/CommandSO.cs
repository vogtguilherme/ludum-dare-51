using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Command", menuName = "Command Settings/Create Command Asset")]
public class CommandSO : ScriptableObject
{
    public string commandKey;
    public CommandType commandType;
    public Command commandObj {get; private set;}

    public Command CreateCommand(CommandManager manager)
    {
        switch (commandType)
        {
            case CommandType.TRANSLATION:
                commandObj = new MoveCommand(manager, commandKey, ValueType.numeric);
                break;
            case CommandType.ROTATION:
                commandObj = new RotateCommand(manager, commandKey, ValueType.numeric);
                break;
            case CommandType.RECON:
                commandObj = new ReconCommand(manager, commandKey, ValueType.text);
                break;
            default:
                break;
        }

        return commandObj;
    }
}

public enum CommandType
{
    TRANSLATION, ROTATION, RECON
}