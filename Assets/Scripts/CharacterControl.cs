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
    Animator animat;
    KeyCode k;
    Dictionary<KeyCode, Punch> punches;
    bool isPunching = false;
    bool damageApplied = false;

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
            if (Input.GetKeyDown(key))
            {
                MakePunch(key);
                k = key;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (isPunching && !damageApplied)
        {
            Action.ApplyDamage(punches[k], collision);
            damageApplied = true;
        }
    }

    public void AnimationEnd(string message)
    {
        if (message.Equals("AnimationEnded")) 
        { 
            isPunching = false;
            damageApplied = false;
        }
    }
    public void MakePunch(KeyCode key)
    {
        if (characteristics.Stamina >= punches[key].staminaDamage) characteristics.Stamina -= punches[key].staminaDamage;
        else
        {
            characteristics.Stamina = 0;
            punches[key].damage = punches[key].maxDamage / 2;
            anim[punches[key].anim.name].speed = 0.5f;
        }
        if (!anim.isPlaying)
        {
            anim.Play(punches[key].anim.name);
            isPunching = true;
        }
    }
}
