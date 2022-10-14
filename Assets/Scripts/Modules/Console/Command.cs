using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public string key;
    public ValueType valueType;

    public abstract AsyncOperation OnExecute(GameObject target);
}

public enum ValueType
{
    numeric, text
}