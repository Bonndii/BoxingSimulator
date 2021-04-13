using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    [SerializeField] private float fightTimer = 180f;
    [SerializeField] private float pauseTimer = 60f;
    [SerializeField] private float fightCooldown = 180f;
    [SerializeField] private float pauseCooldown = 60f;

    void Update()
    {
        if (fightTimer > 0)
        {
            fightTimer -= Time.deltaTime;
        }
        else
        {
            if (pauseTimer > 0)
            {
                pauseTimer -= Time.deltaTime;   
            }
            
            
            fightTimer = fightCooldown;
            pauseTimer = pauseCooldown;
        }
    }
}
