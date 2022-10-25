using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public string key { get; protected set; }
    public string[] instructions { get; protected set; }
    public ValueType valueType { get; protected set; }

    protected CommandManager manager = null;

    public void SetInstructions(string[] instructions)
    {
        this.instructions = instructions;
        foreach (string instruction in instructions)
        {
            Debug.Log(instruction);
        }
    }

    public IEnumerator OnExecute;
}

public enum ValueType
{
    numeric, text
}