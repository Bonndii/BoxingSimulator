using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Punch
{
    public int maxDamage;
    public int damage;
    public int staminaDamage;
    public int enemyStaminaDamage;
    public AnimationClip anim;
    public PunchType type;

    public Punch(int maxDmg, int stDmg, AnimationClip an, PunchType t, int enStDmg = 0)
    {
        maxDamage = maxDmg;
        damage = maxDmg;
        staminaDamage = stDmg;
        enemyStaminaDamage = enStDmg;
        anim = an;
        type = t;
    }
}
public class CharacterAction : MonoBehaviour
{
    [SerializeField]
    private Characteristics characteristics;
    public void ApplyDamage(Punch punch, Collider collision)
    {
        Characteristics enemy = collision.gameObject.GetComponentInParent<Characteristics>();
        float dmgMulti = characteristics.StaminaDmgMulti.Evaluate((enemy.MaxStamina - enemy.Stamina) / 100f);
        if ((int)punch.type == (int)enemy.BlockType)
        {
            if (enemy.Block >= punch.damage) 
            {
                enemy.Block -= punch.damage * dmgMulti;
            }
            else
            {
                enemy.Health -= punch.damage*dmgMulti - enemy.Block;
                enemy.Block = 0;
            }

        }
        else
        {
            enemy.Health -= punch.damage * dmgMulti;
            if (punch.type == PunchType.Lower)
            {
                if(enemy.Stamina >= punch.enemyStaminaDamage) enemy.Stamina -= punch.enemyStaminaDamage;
                else enemy.Stamina = 0;
            }
        }
        enemy.Timer = enemy.Cooldown;
    }
}
    

