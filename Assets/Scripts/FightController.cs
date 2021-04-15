using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentStatus
{
    Pause, Fight
}
public class FightController : MonoBehaviour
{
    [SerializeField]
    Characteristics c1, c2;
    [SerializeField]
    CurrentStatus status;
    [SerializeField] 
    private float fightTimer = 180f;
    [SerializeField] 
    private float pauseTimer = 60f;
    [SerializeField] 
    private float fightCooldown = 180f;
    [SerializeField] 
    private float pauseCooldown = 60f;
    [SerializeField]
    private int rounds = 1;
    [SerializeField]
    private GameObject spawn1;
    [SerializeField]
    private GameObject spawn2;


    public CurrentStatus Status
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
        }
    }

    private void Start()
    {

    }

    void Update()
    {
        if (fightTimer > 0)
        {
            fightTimer -= Time.deltaTime;
            status = CurrentStatus.Fight;
        }
        else
        {
            if (pauseTimer > 0)
            {
                if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                {
                    pauseTimer = 0;
                }
                pauseTimer -= Time.deltaTime;
                status = CurrentStatus.Pause;
                c2.transform.position = spawn1.transform.position;
                c1.transform.position = spawn2.transform.position;
            }
            else
            {
                fightTimer = fightCooldown;
                pauseTimer = pauseCooldown;
                c1.RegenStats();
                c2.RegenStats();
                if (rounds < 12)
                {
                    rounds += 1;
                }
            }
        }
    }
}
