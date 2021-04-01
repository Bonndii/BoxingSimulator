using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlace : MonoBehaviour
{
    [SerializeField]
    private GameObject place1;
    [SerializeField]
    private GameObject place2;

    public void OnClick()
    {
        place1.SetActive(false);
        place2.SetActive(true);
    }
}
