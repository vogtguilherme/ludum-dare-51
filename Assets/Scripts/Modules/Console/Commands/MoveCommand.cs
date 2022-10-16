using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public MoveCommand(CommandManager manager, string key, ValueType valueType)
    {
        this.key = key;
        this.valueType = valueType;
        this.manager = manager;
    }
    
    public override AsyncOperation OnExecute(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}