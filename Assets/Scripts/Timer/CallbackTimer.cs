using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackTimer
{
    private static List<CallbackTimer> activeTimers = new List<CallbackTimer>();
    private static GameObject initGameObject;
    
    public static CallbackTimer SetTimer(Action action, float seconds, string timerName = null)
    {
        GameObject gameObject = new GameObject("Timer", typeof(UpdateComponent));
        CallbackTimer callbackTimer = new CallbackTimer(action, seconds, gameObject);
        gameObject.GetComponent<UpdateComponent>().onUpdate = callbackTimer.Update;
        activeTimers.Add(callbackTimer);
        return callbackTimer;
    }
    
    private static void RemoveTimer(CallbackTimer callbackTimer)
    {
        activeTimers.Remove(callbackTimer);
    }
    public class UpdateComponent : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    }
    private Action action;
    private float delay;
    private bool isDestroyed;
    private GameObject gameObject;
    private string name;
    private CallbackTimer(Action callback, float seconds, GameObject timerObject)
    {
        action = callback;
        delay = seconds;
        isDestroyed = false;
        gameObject = timerObject;
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                action();
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
    }
}
