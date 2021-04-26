using UnityEngine;

[CreateAssetMenu(menuName = "Punch", fileName = "NewPunch")]
public class Punch : ScriptableObject
{
    public float maxDamage;
    public float damage;
    public float staminaDamage;
    public float enemyStaminaDamage;
    public AnimationClip anim;
    public PunchType type;
    public KeyCode key;
}
