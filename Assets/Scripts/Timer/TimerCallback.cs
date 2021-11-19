using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerCallback : MonoBehaviour
{
    private float delay;
    private float elapsed = 0;
    private bool done = true;
    private Action action;
    private bool loop = true;
    public void SetTimer(float seconds, Action callback)
    {
        done = false;
        delay = seconds;
        action = callback;
    }
    private void Update()
    {
        if (!done)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= delay)
            {
                elapsed = 0;
                done = true;
                action();
                if(loop)
                    SetTimer(delay, action);
            }
        }
    }
}
