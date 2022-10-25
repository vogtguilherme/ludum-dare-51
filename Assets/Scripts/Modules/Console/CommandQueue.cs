using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandQueue : MonoBehaviour
{
    Command currentCommand = null;
    Queue<Command> queuedCommands = new Queue<Command>();

    private Action<Command> OnEnqueue, OnDequeue;

    private bool onExecution = false;

    public bool OnExecution { get { return onExecution; } }

    private void Start()
    {
        OnEnqueue += EnqueueLog;
        OnDequeue += DequeueLog;
    }

    private void OnDestroy()
    {
        OnEnqueue -= EnqueueLog;
        OnDequeue -= DequeueLog;
    }

    public void Enqueue(Command cmd)
    {
        queuedCommands.Enqueue(cmd);

        if (!onExecution)
        {
            StartCoroutine(RunQueue());
        }

        OnEnqueue?.Invoke(cmd);
    }

    public void Dequeue(Command cmd)
    {
        if (queuedCommands.Count > 0)
        {
            queuedCommands.Dequeue();
            OnDequeue?.Invoke(cmd);
        }
    }

    IEnumerator RunQueue()
    {
        onExecution = true;
        currentCommand = null;

        while (queuedCommands.Count > 0)
        {
            currentCommand = queuedCommands.Peek();

            yield return currentCommand.OnExecute;

            Dequeue(currentCommand);
            currentCommand = null;            
        }

        onExecution = false;

        yield return null;
    }

    void EnqueueLog(Command command)
    {
        string message = "Enqueueing command: " + command.key;
        Debug.Log(message);
        CommandManager.Instance.LogMessage(message, LogType.Message);
    }

    void DequeueLog(Command command)
    {
        string message = "Dequeueing command: " + command.key;
        Debug.Log(message);
        CommandManager.Instance.LogMessage(message, LogType.Message);
    }
}