using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayConfiguration
{
    public event Action OnCountOver;

    private float m_InputDelay;
    private float m_ElapsedTime = 0;

    public float ElapsedTime
    {
        get { return m_ElapsedTime; }
    }

    public IEnumerator InitCount(float delay)
    {
        yield return PerformCount();
    }

    private IEnumerator PerformCount()
    {
        m_ElapsedTime = 0;

        while (m_ElapsedTime < m_InputDelay)
        {
            m_ElapsedTime+= Time.deltaTime;
            yield return null;
        }

        OnCountOver?.Invoke();
    }
}
