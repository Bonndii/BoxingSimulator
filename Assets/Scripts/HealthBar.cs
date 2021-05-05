using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    void Awake()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}
