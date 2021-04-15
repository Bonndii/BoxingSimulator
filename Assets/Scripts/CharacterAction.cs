using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Punch
{
    public float maxDamage;
    public float damage;
    public float staminaDamage;
    public float enemyStaminaDamage;
    public AnimationClip anim;
    public PunchType type;
    public KeyCode key;

    public Punch(float maxDmg, float stDmg, AnimationClip an, PunchType t, KeyCode k, float enStDmg = 0)
    {
        maxDamage = maxDmg;
        damage = maxDmg;
        staminaDamage = stDmg;
        enemyStaminaDamage = enStDmg;
        anim = an;
        type = t;
        key = k;
    }
}
public class CharacterAction : MonoBehaviour
{
    [SerializeField]
    private Characteristics characteristics;
    public void ApplyDamage(Punch punch, Collider collision)
    {
        Characteristics enemy = collision.gameObject.GetComponentInParent<Characteristics>();
        float dmgMulti = enemy.StaminaDmgMulti.Evaluate((enemy.MaxStamina - enemy.Stamina) * 0.01f);
        if ((int)punch.type == (int)enemy.BlockType)
        {
            if (enemy.Block >= punch.damage) 
            {
                enemy.Block -= punch.damage;
                enemy.BlockTaken += punch.damage;
            }
            else
            {
                if(enemy.Health - punch.damage * dmgMulti + enemy.Block >=0)
                {
                    enemy.Health -= punch.damage * dmgMulti - enemy.Block;
                    enemy.BlockTaken += enemy.Block;
                    enemy.DamageTaken += punch.damage * dmgMulti - enemy.Block;
                    enemy.Block = 0;
                    characteristics.Points += 1;
                }
                else
                {
                    enemy.BlockTaken += enemy.Block;
                    enemy.DamageTaken += punch.damage * dmgMulti - enemy.Block;
                    enemy.Health = 0;
                    enemy.Block = 0;
                    characteristics.Points += 1;
                }              
            }
        }
        else
        {
            if(enemy.Health - punch.damage * dmgMulti >= 0)
            {
                enemy.Health -= punch.damage * dmgMulti;
                enemy.DamageTaken += punch.damage * dmgMulti;
                characteristics.Points += 1;
            }
            else
            {
                enemy.DamageTaken += enemy.Health;
                enemy.Health = 0;
                characteristics.Points += 1;
            }            
            if (punch.type == PunchType.Lower)
            {
                if (enemy.Stamina >= punch.enemyStaminaDamage)
                {
                    enemy.Stamina -= punch.enemyStaminaDamage;
                    enemy.StaminaTaken += punch.enemyStaminaDamage;
                }
                else 
                {
                    enemy.StaminaTaken += enemy.Stamina;
                    enemy.Stamina = 0; 
                }
            }
        }
        enemy.Timer = enemy.Cooldown;
    }
}
    

