using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShower : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] bool isShow;

    public void ToggleShow()
    {
        if (isShow)
        {
            Color color = material.color;
            color.a = 0;
            material.color = color;
        }
        else
        {
            Color color = material.color;
            color.a = 0.2f;
            material.color = color;
        }
    }
}
