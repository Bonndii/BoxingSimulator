using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentStatus
{
    Pause, Fight
}
public class FightController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fightTimer = 180f;
    [SerializeField] private float pauseTimer = 60f;
    [SerializeField] private float fightCooldown = 180f;
    [SerializeField] private float pauseCooldown = 60f;

    [Header("References")]
    [SerializeField] private GameObject spawn1;
    [SerializeField] private GameObject spawn2;
    [SerializeField] private Characteristics first;
    [SerializeField] private Characteristics second;

    private int rounds = 1;



    private void Update()
    {
        if (fightTimer > 0)
        {
            fightTimer -= Time.deltaTime;
            SetStatus(CurrentStatus.Fight);
            return;
        }

        if (pauseTimer > 0)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                pauseTimer = 0;

            pauseTimer -= Time.deltaTime;
            SetStatus(CurrentStatus.Pause);

            second.transform.position = spawn1.transform.position;
            first.transform.position = spawn2.transform.position;

            return;
        }

        fightTimer = fightCooldown;
        pauseTimer = pauseCooldown;

        first.RegenStats();
        second.RegenStats();

        if (rounds < 12)
            rounds += 1;
    }

    private void SetStatus(CurrentStatus status)
    {
        first.isFighting = status == CurrentStatus.Fight;
        second.isFighting = status == CurrentStatus.Fight;
    }
}