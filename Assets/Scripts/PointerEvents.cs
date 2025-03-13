using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerEvents : MonoBehaviour
{
    float t;
    public RectTransform donut;
    public GameObject donutPrefab;

    public void whenMouseEnter()
    {
        donut.localScale = Vector3.one * 1.5f;
    }

    public void whenMouseLeft()
    {
        donut.localScale = Vector3.one;
    }

    public void whenMouseClick()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        GameObject instance = Object.Instantiate(donutPrefab, mouseScreenPosition, Quaternion.identity);
    }
}
