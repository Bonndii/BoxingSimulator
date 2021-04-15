using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PunchType
{
    None, Upper, Lower
}
public enum BlockType
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

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float block;
    [SerializeField]
    private float maxBlock;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float maxStamina;
    [SerializeField]
    BlockType blockType;
    float timer;
    float staminaTimer;
    float cooldown = 3;
    [SerializeField]
    private AnimationCurve staminaDmgMulti;
    [SerializeField]
    private int points = 0;
    [SerializeField]
    private float damageTaken;
    [SerializeField]
    private float staminaTaken;
    [SerializeField]
    private float blockTaken;
    [SerializeField]
    private float t;
    [SerializeField]
    private FightController fightController;

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
    public float DamageTaken
    {
        get
        {
            return damageTaken;
        }
        set
        {
            damageTaken = value;
        }
    }
    public float StaminaTaken
    {
        get
        {
            return staminaTaken;
        }
        set
        {
            staminaTaken = value;
        }
    }
    public float BlockTaken
    {
        get
        {
            return blockTaken;
        }
        set
        {
            blockTaken = value;
        }
    }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    public float Block
    {
        get
        {
            return block;
        }
        set
        {
            block = value;
        }
    }

    public float MaxBlock
    {
        get
        {
            return maxBlock;
        }
        set
        {
            maxBlock = value;
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
        }
    }

    public float MaxStamina
    {
        get
        {
            return maxStamina;
        }
        set
        {
            maxStamina = value;
        }
    }

    public float Timer
    {
        get
        {
            return timer;
        }
        set
        {
            timer = value;
        }
    }

    public float StaminaTimer
    {
        get
        {
            return staminaTimer;
        }
        set
        {
            staminaTimer = value;
        }
    }

    public float Cooldown
    {
        get
        {
            return cooldown;
        }
        set
        {
            cooldown = value;
        }
    }

    public BlockType BlockType
    {
        get
        {
            return blockType;
        }
        set
        {
            blockType = value;
        }
    }

    public AnimationCurve StaminaDmgMulti
    {
        get
        {
            return staminaDmgMulti;
        }
        set
        {
            staminaDmgMulti = value;
        }
    }

    public void Update()
    {
        if (fightController.Status == CurrentStatus.Fight)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (staminaTimer > 0)
            {
                staminaTimer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                t += Time.deltaTime;
                maxHealth -= damageTaken / 10f;
                damageTaken = 0f;
                maxStamina -= staminaTaken / 10f;
                staminaTaken = 0f;
                maxBlock -= blockTaken / 10f;
                blockTaken = 0f;
                if (health < maxHealth)
                {
                    health += 500 * Time.deltaTime;
                }
                if (block < maxBlock)
                {
                    block += 500 * Time.deltaTime;
                }
                if (stamina < maxStamina && staminaTimer <= 0)
                {
                    stamina += 500 * Time.deltaTime;
                }
            }
        }
        if(fightController.Status == CurrentStatus.Pause)
        {
            t = 0;
            maxHealth -= damageTaken / 10f;
            damageTaken = 0f;
            maxStamina -= staminaTaken / 10f;
            staminaTaken = 0f;
            maxBlock -= blockTaken / 10f;
            blockTaken = 0f;
        }
    }

    public void RegenStats()
    {
        if(maxHealth + baseHeatlhRegen* t/60 <= maxMaxHealth)
        {
            maxHealth += baseHeatlhRegen * t / 60;
            health = maxHealth;
        }
        else
        {
            maxHealth = maxMaxHealth;
            health = maxHealth;
        }
        if (maxBlock + baseBlockRegen * t / 60 <= maxMaxBlock)
        {
            maxBlock += baseBlockRegen * t / 60;
            block = maxBlock;
        }
        else
        {
            maxBlock = maxMaxBlock;
            block = maxBlock;
        }
        if (maxStamina + baseStaminaRegen * t / 60 <= maxMaxStamina)
        {
            maxStamina += baseStaminaRegen * t / 60;
            stamina = maxStamina;
        }
        else
        {
            maxStamina = maxMaxStamina;
            stamina = maxStamina;
        }
    }
}
