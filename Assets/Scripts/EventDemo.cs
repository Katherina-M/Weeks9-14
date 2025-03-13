using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;
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
