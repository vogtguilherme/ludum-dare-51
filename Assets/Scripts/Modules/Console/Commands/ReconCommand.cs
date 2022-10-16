using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconCommand : Command
{
    public ReconCommand(CommandManager manager, string key, ValueType valueType)
    {
        this.manager = manager;
        this.key = key;
        this.valueType = valueType;
    }
    
    public override AsyncOperation OnExecute(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}