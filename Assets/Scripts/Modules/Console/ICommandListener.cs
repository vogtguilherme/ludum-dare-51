using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandListener
{
    void UpdateListener(Command sender);
}