using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentStatus
{
    UpperAttack, UpperBlock, LowerAttack, LowerBlock, Move, Nothing
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
    private int leftStraightDamage;
    [SerializeField]
    private int rightStraightDamage;
    [SerializeField]
    private int leftStraightEnergyDamage;
    [SerializeField]
    private int rightStraightEnergyDamage;
    [SerializeField]
    private int leftStraightMaxDamage;
    [SerializeField]
    private int rightStraightMaxDamage;
    [SerializeField]
    public CurrentStatus currentStatus;

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
    public int LeftStraightDamage
    {
        get
        {
            return leftStraightDamage;
        }
        set
        {
            leftStraightDamage = value;
        }
    }

    public int RightStraightDamage
    {
        get
        {
            return rightStraightDamage;
        }
        set
        {
            rightStraightDamage = value;
        }
    }

    public int LeftStraightEnergyDamage
    {
        get
        {
            return leftStraightEnergyDamage;
        }
        set
        {
            leftStraightEnergyDamage = value;
        }
    }

    public int RightStraightEnergyDamage
    {
        get
        {
            return rightStraightEnergyDamage;
        }
        set
        {
            rightStraightEnergyDamage = value;
        }
    }
    public int LeftStraightMaxDamage
    {
        get
        {
            return leftStraightMaxDamage;
        }
        set
        {
            leftStraightMaxDamage = value;
        }
    }

    public int RightStraightMaxDamage
    {
        get
        {
            return rightStraightMaxDamage;
        }
        set
        {
            rightStraightMaxDamage = value;
        }
    }
}
