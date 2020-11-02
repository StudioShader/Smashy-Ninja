using UnityEngine;
using System.Collections;

public class Guardsman : Unit {
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
        if (Player.transform.position.x >= transform.position.x - dX2 && Player.transform.position.x <= transform.position.x + 0.25f && Player.transform.position.y <= transform.position.y + dY && Player.transform.position.y >= transform.position.y - 0.3f && !atacked && !dead)
        {
            Atack();
        }
        if (Player.transform.position.x >= transform.position.x - dX2A && Player.transform.position.x <= transform.position.x + 0.25f && Player.transform.position.y <= transform.position.y + dY && Player.transform.position.y >= transform.position.y- 0.3f && !atacked && !dead)
        {
            anim.SetBool("atack", true);
        }
        else
        {
            anim.SetBool("atack", false);
        }
    }
    public override void Atack()
    {
        Player.GetComponent<Player>().RecieveDamage(1);
        atacked = true;
    }
    public override void Death()
    {
        anim.SetBool("Dead", true);
        Invoke("StopBlood", 2f);
        //SpawnParticles(particle, 10);
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
