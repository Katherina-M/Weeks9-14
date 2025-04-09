using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibilityItem : MonoBehaviour
{
    private bool hasSetPosition = false;

    void Start()
    {
        if (!hasSetPosition)
        {
            float x = Mathf.Round(Random.Range(-8f, 8f));
            float y = Mathf.Round(Random.Range(-4f, 4f));
            transform.position = new Vector2(x, y);
            hasSetPosition = true;
        }
    }
}
