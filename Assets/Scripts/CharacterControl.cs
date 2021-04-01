using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    private CharacterAction Action;
    [SerializeField]
    private Characteristics characteristics;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float speed;
    private int damage;
    private Collider boxerCollider;
    Animation anim;

    void Start()
    {
        boxerCollider = GetComponent<Collider>();
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * (-x) + transform.forward * (-z);
        controller.Move(move * speed * Time.deltaTime);
        SetCurrentStatus();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "BoxerHead")
        {
            Action.UpperEnemyHit(collision ,damage);
            characteristics.currentStatus = CurrentStatus.Nothing;
        }
        else if (collision.gameObject.tag == "BoxerChest")
        {
            Action.LowerEnemyHit(collision, damage, damage);
            characteristics.currentStatus = CurrentStatus.Nothing;
        }
    }

    public void SetCurrentStatus()
    {
        if (Input.GetMouseButtonDown(0) && anim.isPlaying != true)
        {
            characteristics.currentStatus = CurrentStatus.UpperAttack;
            damage = characteristics.LeftStraightDamage;
            Action.EnergyHit(characteristics.LeftStraightEnergyDamage);
            anim.Play("LeftStraight");
        }
        else if (Input.GetMouseButtonDown(1) && anim.isPlaying != true)
        {
            characteristics.currentStatus = CurrentStatus.LowerAttack;
            damage = characteristics.RightStraightDamage;
            Action.EnergyHit(characteristics.RightStraightEnergyDamage);
            anim.Play("RightStraight");
        }
    }
}
