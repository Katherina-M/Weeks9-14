using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;

    public UnityEvent OnTunerHasFinished;
    public float timerLength = 3;
    public float t;

    private void Update()
    {
        t += Time.deltaTime;
        if (t > timerLength)
        {
            t = 0;
            OnTunerHasFinished.Invoke();
        }
    }

    public void mouseJustEnterImage()
    {
        Debug.Log("Mouse just enter me!");
        banana.localScale = Vector3.one * 1.2f;

    }

    public void mouseJustLeftImage()
    {
        Debug.Log("Mouse just left me!");
        banana.localScale = Vector3.one;
    }
}
