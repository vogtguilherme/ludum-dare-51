using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField]
    private CommandParser m_CommandParser = null;
    [SerializeField]
    private List<string> m_Commands = new List<string>();

    private CommandValidator m_Validator = null;

    public CommandValidator Validator
    {
        get { return m_Validator; }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_Validator = new CommandValidator(m_Commands);
        m_CommandParser.OnCommandParsed += OnCommandParsed;
    }

    private void OnCommandParsed(string[] command)
    {
        if (m_Validator != null)
        {
            if (m_Validator.ValidateCommandAction(command[0]))
            {
                Debug.Log(command[0] + " " + command[1]);
            }
            else
            {
                Debug.Log("Command not found");
            }
        }
    }
}
