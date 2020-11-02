using UnityEngine;
using System.Collections;

public class Mage : Unit {
    public bool atacked = false;

    private GameObject Player;

    public Animator anim;

    [SerializeField]
    private float dY, dX1, dX2, dX2A;

    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        particle = Resources.Load<GameObject>("Particles/Particle2");
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Update()
    {
        if (Health <= 0 && !dead)
        {
            dead = true;
            Death();
        }
    }
    public override void Death()
    {
        anim.SetBool("Dead", true);
        transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
        transform.GetChild(2).gameObject.GetComponent<Collider2D>().enabled = false;
        transform.GetChild(3).gameObject.GetComponent<Collider2D>().enabled = false;
        transform.GetChild(4).gameObject.GetComponent<Collider2D>().enabled = false;
        transform.GetChild(1).gameObject.GetComponentInChildren<Animator>().SetBool("Dead", true);
        transform.GetChild(2).gameObject.GetComponentInChildren<Animator>().SetBool("Dead", true);
        transform.GetChild(3).gameObject.GetComponentInChildren<Animator>().SetBool("Dead", true);
        transform.GetChild(4).gameObject.GetComponentInChildren<Animator>().SetBool("Dead", true);
        //GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Arrow arrow = other.gameObject.GetComponent<Arrow>();
        if (arrow && !arrow.Bad)
        {
            RecieveDamage(1);
        }
    }

}
