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
    KeyCode k;
    Dictionary<KeyCode, Punch> punches;
    bool isPunching = false;

    void Start()
    {
        boxerCollider = GetComponent<Collider>();
        anim = GetComponent<Animation>();
        punches = new Dictionary<KeyCode, Punch>();
        punches.Add(KeyCode.Mouse0, new Punch(1000, 1000, anim.GetClip("LeftStraight"), PunchType.Upper));
        punches.Add(KeyCode.Mouse1, new Punch(2000, 2000, anim.GetClip("RightStraight"), PunchType.Lower, 2000));
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * (-x) + transform.forward * (-z);
        controller.Move(move * speed * Time.deltaTime);
        foreach(KeyCode key in punches.Keys)
        {
            if (Input.GetKeyDown(key) && !anim.isPlaying)
            {
                MakePunch(key);
                k = key;
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
    public void MakePunch(KeyCode key)
    {
        if (characteristics.Stamina >= punches[key].staminaDamage) characteristics.Stamina -= punches[key].staminaDamage;
        else
        {
            characteristics.Stamina = 0;
            punches[key].damage = punches[key].maxDamage / 2;
            animSpeed = 0.5f;
        }
        anim[punches[key].anim.name].speed = animSpeed;
        anim.Play(punches[key].anim.name);
        isPunching = true;
        characteristics.StaminaTimer = characteristics.Cooldown;
    }
}
