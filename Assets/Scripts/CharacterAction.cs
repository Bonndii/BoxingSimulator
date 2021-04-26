using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    [SerializeField] private Characteristics characteristics;

    public void ApplyDamage(Punch punch, Characteristics enemy)
    {
        if (enemy.TakeDamage(punch))
            characteristics.points++;
    }
}
    

