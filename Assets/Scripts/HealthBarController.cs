using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Characteristics characteristics;

    // Update is called once per frame
    void Update()
    {
        healthBar.SetSize(characteristics.health / characteristics.MaxHealth);
    }
}
