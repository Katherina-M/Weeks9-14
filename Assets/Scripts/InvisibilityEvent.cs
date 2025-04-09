using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class InvisibilityEvent : MonoBehaviour
{
    public class InvisibilityEvents : UnityEvent { }

    public InvisibilityEvents OnPlayerInvisible;

    private void Start()
    {
        if (OnPlayerInvisible == null)
        {
            OnPlayerInvisible = new InvisibilityEvents();
        }
    }

    public void TriggerInvisibility()
    {
        Debug.Log("Invisiable triggered, Tell all the listener");
        OnPlayerInvisible.Invoke();
    }
}
