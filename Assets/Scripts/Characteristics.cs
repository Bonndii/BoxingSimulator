using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PunchType
{
    None, Upper, Lower
}


public class Characteristics : MonoBehaviour
{
    private const float maxMaxHealth = 100000f;
    private const float maxMaxBlock = 5000f;
    private const float maxMaxStamina = 1000f;
    private const float minMaxHealth = 10000f;
    private const float minMaxBlock = 1000f;
    private const float minMaxStamina = 100f;
    private const float baseHeatlhRegen = 10000f;
    private const float baseStaminaRegen = 1000f;
    private const float baseBlockRegen = 1000f;

    [Header("Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxBlock;
    [SerializeField] private float maxStamina;
    public AnimationCurve staminaDmgMulti;

    [Header("Score")]
    public int points;

    [HideInInspector] public bool isFighting;
     public float health;
     public float block;
     public float stamina;
    [HideInInspector] public float damageTaken;
    [HideInInspector] public float staminaTaken;
    [HideInInspector] public float blockTaken;
    public PunchType blockType;

    public float EvaluatedStamina => staminaDmgMulti.Evaluate((maxStamina - stamina) * 0.01f);

    private float timer;
    private float staminaTimer;
    private float cooldown = 3;
    private float t;

    private Animator animat;

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public float MaxBlock
    {
        get
        {
            return maxBlock;
        }
    }

    public float MaxStamina
    {
        get
        {
            return maxStamina;
        }
    }

    private void Start()
    {
        animat = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isFighting)
        {
            if (staminaTimer > 0)
                staminaTimer -= Time.deltaTime;

            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                t += Time.deltaTime;
                ApplyTakenDamage();

                if (health < maxHealth) health += 500 * Time.deltaTime;

                if (block < maxBlock) block += 500 * Time.deltaTime;

                if (stamina < maxStamina && staminaTimer <= 0) stamina += 500 * Time.deltaTime;
            }
        }
        else
        {
            t = 0;
            ApplyTakenDamage();
        }
    }



    public void RegenStats()
    {
        maxHealth = CalculateNewValue(maxHealth, maxMaxHealth, baseHeatlhRegen);
        health = maxHealth;

        maxBlock = CalculateNewValue(maxBlock, maxMaxBlock, baseBlockRegen);
        block = maxBlock;

        maxStamina = CalculateNewValue(maxStamina, maxMaxStamina, baseStaminaRegen);
        stamina = maxStamina;
    }

    public bool TakeDamage(Punch punch)
    {
        timer = cooldown;

        if (punch.type == blockType)
        {
            animat.Play($"Base Layer.Center Block");

            if (block >= punch.damage)
            {
                block -= punch.damage;
                blockTaken += punch.damage;

                return false;
            }
            else
            {
                if (health - punch.damage * EvaluatedStamina + block >= 0)
                {
                    health -= punch.damage * EvaluatedStamina - block;
                    damageTaken += punch.damage * EvaluatedStamina - block;
                }
                else
                {
                    damageTaken += health - block;
                    health = 0;
                }

                blockTaken += block;
                block = 0;
                
                return true;
            }
        }
        else
        {
            if (punch.type == PunchType.Lower)
            {
                animat.Play($"Base Layer.Hit To Body");

                if (stamina >= punch.enemyStaminaDamage)
                {
                    stamina -= punch.enemyStaminaDamage;
                    staminaTaken += punch.enemyStaminaDamage;
                }
                else
                {
                    staminaTaken += stamina;
                    stamina = 0;
                }
            }
            else
            {
                animat.Play($"Base Layer.Head Hit");
            }

            if (health - punch.damage * EvaluatedStamina >= 0)
            {
                health -= punch.damage * EvaluatedStamina;
                damageTaken += punch.damage * EvaluatedStamina;
            }
            else
            {
                damageTaken += health;
                health = 0;
            }            

            return true;
        }
    }

    public void ResetStaminaTimer()
    {
        staminaTimer = cooldown;
    }


    private void ApplyTakenDamage()
    {
        maxHealth -= damageTaken / 10f;
        damageTaken = 0f;

        maxStamina -= staminaTaken / 10f;
        staminaTaken = 0f;

        maxBlock -= blockTaken / 10f;
        blockTaken = 0f;
    }

    private float CalculateNewValue(float currentValue, float maxValue, float valueRegen)
    {
        return currentValue + valueRegen * t / 60 <= maxValue
            ? currentValue + valueRegen * t / 60
            : maxValue;
    }
}
