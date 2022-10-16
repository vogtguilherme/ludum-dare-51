using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputReader : MonoBehaviour
{
    private InputField m_TextInput = null;

    public event Action<string> OnEndEdit;

    private void Awake()
    {
        m_TextInput = GetComponent<InputField>();
        OnEndEdit += OnInputSent;
    }

    public void GetInput()
    {
        string _entry = m_TextInput.text;
        if (string.IsNullOrEmpty(_entry)) return;
        //Debug.Log(_entry);
        OnEndEdit?.Invoke(_entry);
    }

    private void OnInputSent(string entry)
    {
        m_TextInput.text = string.Empty;
        m_TextInput.ActivateInputField();
    }
}