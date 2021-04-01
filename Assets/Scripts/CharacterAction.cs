using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    [SerializeField]
    private Characteristics characteristics;
    public void UpperEnemyHit(Collider collision, int damagePoints)
    {
        Characteristics newP = collision.gameObject.GetComponentInParent<Characteristics>();
        if (characteristics.currentStatus == CurrentStatus.UpperAttack && newP.currentStatus != CurrentStatus.UpperBlock)
        {
            if (newP.Health - damagePoints > 0)
            {
                newP.Health -= damagePoints;
            }
            else
            {
                newP.Health = 0;
            }
        }

        else if (characteristics.currentStatus == CurrentStatus.UpperAttack && newP.currentStatus == CurrentStatus.UpperBlock)
        {
            if (newP.Block - damagePoints >= 0)
            {
                newP.Block -= damagePoints;
            }
            else if (newP.Health - damagePoints > 0)
            {
                newP.Block = 0;
                newP.Health -= damagePoints;
            }
            else
            {
                newP.Block = 0;
                newP.Health = 0;
            }   
        }
    }

    public void LowerEnemyHit(Collider collision, int damagePoints, int staminaPoints)
    {
        Characteristics newP = collision.gameObject.GetComponentInParent<Characteristics>();
        if (characteristics.currentStatus == CurrentStatus.LowerAttack && newP.currentStatus != CurrentStatus.LowerBlock)
        {
            if (newP.Health - damagePoints > 0 && newP.Stamina - staminaPoints > 0)
            {
                newP.Health -= damagePoints;
                newP.Stamina -= staminaPoints;
            } 
            else if (newP.Health - damagePoints > 0 && newP.Stamina - staminaPoints < 0)
            {
                newP.Health -= damagePoints;
                newP.Stamina = 0;
            }
            else
            {
                newP.Health = 0;
                newP.Stamina = 0;
            }
        }

        else if (characteristics.currentStatus == CurrentStatus.LowerAttack && newP.currentStatus == CurrentStatus.LowerBlock)
        {
            if (newP.Block - damagePoints >= 0)
            {
                newP.Block -= damagePoints;
            }
            else if (newP.Health - damagePoints > 0 && newP.Stamina - staminaPoints > 0)
            {
                newP.Block = 0;
                newP.Health -= damagePoints;
                newP.Stamina -= staminaPoints;
            }
            else if (newP.Health - damagePoints > 0 && newP.Stamina - staminaPoints < 0)
            {
                newP.Block = 0;
                newP.Health -= damagePoints;
                newP.Stamina = 0;
            }
            else
            {
                newP.Block = 0;
                newP.Health = 0;
                newP.Stamina = 0;
            }
        }
    }

    public void EnergyHit(int energyPoints)
    {
        if(characteristics.Stamina - energyPoints > 0)
        {
            characteristics.Stamina -= energyPoints;
            characteristics.LeftStraightDamage = characteristics.LeftStraightMaxDamage;
            characteristics.RightStraightDamage = characteristics.RightStraightMaxDamage;
        }
        else
        {
            characteristics.LeftStraightDamage = characteristics.LeftStraightMaxDamage / 4;
            characteristics.RightStraightDamage = characteristics.RightStraightMaxDamage / 4;
            characteristics.Stamina = 0;
        }
        
    }
}
    

