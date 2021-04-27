using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class CharacterControl : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;

    [Header("References")]
    [SerializeField] private Characteristics characteristics;
    [SerializeField] private CharacterController controller;

    [Header("Punches")]
    [SerializeField] private Punch[] punches;

    [HideInInspector] public bool isFighting;

    private float animSpeed = 1f;

    private Animation anim;

    private Animator animat;

    private Punch currentPunch;
    
    private void Start()
    {
        anim = GetComponent<Animation>();

        animat = GetComponent<Animator>();
        //punches = new Punch[]
        //{
        //    new Punch(1000, 100, anim.GetClip("LeftStraight"), PunchType.Upper, KeyCode.Mouse0),
        //    new Punch(2000, 200, anim.GetClip("RightStraight"), PunchType.Lower, KeyCode.Mouse1, 200)
        //};
    }

    private void Update()
    {
        if (!characteristics.isFighting)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x == 0 && z == 0) animat.SetBool("isWalking", false);
        else animat.SetBool("isWalking", true);

        Vector3 move = transform.right * (x) + transform.forward * (z);
        controller.Move(move * speed * Time.deltaTime);

        foreach (Punch punch in punches)
        {
            if (Input.GetKeyDown(punch.key) && !animat.GetBool("isPunching"))
            {
                MakePunch(punch);
                animat.SetBool("isPunching", true);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (currentPunch == null)
            return;

        Characteristics enemy = collision.gameObject.GetComponentInParent<Characteristics>();
        if (enemy == null)
            return;

        if (enemy.TakeDamage(currentPunch))
            characteristics.points++;

        animat.SetFloat("animSpeedMulti", -animSpeed);
        animat.Play($"Base Layer.{currentPunch.anim.name}");

        currentPunch = null;
        animat.SetBool("isPunching", false);
    }

    public void AnimationEnd(string message)
    {
        if (message.Equals("AnimationEnded"))
        {
            currentPunch = null;
            animat.SetBool("isPunching", false);
        }
    }

    public void MakePunch(Punch punch)
    {
        currentPunch = punch;

        if (characteristics.stamina >= currentPunch.staminaDamage) 
        {
            characteristics.stamina -= currentPunch.staminaDamage;
            animSpeed = 1f;
        }
        else
        {
            characteristics.stamina = 0;
            currentPunch.damage = currentPunch.maxDamage * 0.5f;
            animSpeed = 0.5f;
        }

        animat.Play($"Base Layer.{currentPunch.anim.name}");
        animat.SetFloat("animSpeedMulti", animSpeed);
        
        characteristics.ResetStaminaTimer();
    }
}
