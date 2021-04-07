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
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int block;
    [SerializeField]
    private int maxBlock;
    [SerializeField]
    private int stamina;
    [SerializeField]
    private int maxStamina;
    [SerializeField]
    public BlockType blockType;

    public int Health
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

    public int MaxHealth
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

    public int Block
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

    public int MaxBlock
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

    public int Stamina
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

    public int MaxStamina
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
}
