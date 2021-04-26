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

    private Punch currentPunch;


    private void Start()
    {
        anim = GetComponent<Animation>();

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

        Vector3 move = transform.right * (-z) + transform.forward * (x);
        controller.Move(move * speed * Time.deltaTime);

        if (anim.isPlaying)
            return;

        foreach (Punch punch in punches)
        {
            if (Input.GetKeyDown(punch.key))
                MakePunch(punch);
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

        anim[currentPunch.anim.name].speed = -animSpeed;
        anim.Play(currentPunch.anim.name);

        currentPunch = null;
    }

    public void AnimationEnd(string message)
    {
        if (message.Equals("AnimationEnded"))
            currentPunch = null;
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

        anim[currentPunch.anim.name].speed = animSpeed;
        anim.Play(currentPunch.anim.name);
        characteristics.ResetStaminaTimer();
    }
}
