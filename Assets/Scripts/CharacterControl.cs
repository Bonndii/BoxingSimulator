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
    private Collider boxerCollider;
    Animation anim;
    float animSpeed = 1f;
    int k;
    [SerializeField]
    Punch[] punches;
    bool isPunching = false;

    void Start()
    {
        boxerCollider = GetComponent<Collider>();
        anim = GetComponent<Animation>();
        punches = new Punch[12];
        punches[0] = new Punch(1000, 100, anim.GetClip("LeftStraight"), PunchType.Upper, KeyCode.Mouse0);
        punches[1] = new Punch(2000, 200, anim.GetClip("RightStraight"), PunchType.Lower, KeyCode.Mouse1, 200);
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * (-x) + transform.forward * (-z);
        controller.Move(move * speed * Time.deltaTime);
        for (int i = 0; i < 2; i++) 
        {
            if (Input.GetKeyDown(punches[i].key) && !anim.isPlaying)
            {
                MakePunch(i);
                k = i;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (isPunching)
        {
            Action.ApplyDamage(punches[k], collision);
            isPunching = false;
            anim[punches[k].anim.name].speed = -animSpeed;
            anim.Play(punches[k].anim.name);
        }
    }

    public void AnimationEnd(string message)
    {
        if (message.Equals("AnimationEnded")) 
        { 
            isPunching = false;
        }
    }
    public void MakePunch(int i)
    {
        if (characteristics.Stamina >= punches[i].staminaDamage) 
        { 
            characteristics.Stamina -= punches[i].staminaDamage;
            animSpeed = 1f;
        }
        else
        {
            characteristics.Stamina = 0;
            punches[i].damage = punches[i].maxDamage * 0.5f;
            animSpeed = 0.5f;
        }
        anim[punches[i].anim.name].speed = animSpeed;
        anim.Play(punches[i].anim.name);
        isPunching = true;
        characteristics.StaminaTimer = characteristics.Cooldown;
    }
}
