using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandSender
{
    void AttachListener(ICommandListener listener);

    void RemoveListener(ICommandListener listener);

    void NotifyListener();
}