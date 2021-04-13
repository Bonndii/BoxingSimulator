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

public class Characteristics : LoaderController<Characteristics>
{
   
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float initialMaxHealth;
    [SerializeField]
    private float block;
    [SerializeField]
    private float maxBlock;
    [SerializeField]
    private float initialMaxBlock;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float maxStamina;
    [SerializeField]
    private float initialMaxStamina;
    BlockType blockType;
    float timer;
    float staminaTimer;
    float cooldown = 3;
    [SerializeField]
    private AnimationCurve staminaDmgMulti;
    private int points = 0;

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
}
