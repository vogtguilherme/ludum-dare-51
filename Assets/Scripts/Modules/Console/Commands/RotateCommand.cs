using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : Command
{
    public RotateCommand(CommandManager manager, string key, ValueType valueType)
    {
        this.key = key;
        this.valueType = valueType;
        this.manager = manager;
    }
}