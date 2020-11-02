using UnityEngine;
using System.Collections;

public class Swidler : Unit {

    private GameObject Player, coin;

    public Animator anim;

    // Use this for initialization
    public void Awake()
    {
        coin = Resources.Load<GameObject>("Particles/coin");
        Health = 5;
        anim = GetComponentInChildren<Animator>();
        particle = Resources.Load<GameObject>("Particles/Particle2");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public override void Update () {
        if(Health == 0)
        {
            Death();
        }
    }

    public override void RecieveDamage(float damage)
    {
        SpawnParticles(coin, 1);
        if (Health >= damage)
        {
            FindObjectOfType<AudioManager>().Play("CoinDrop");
            Health -= damage;
        }
        else
        {
            Death();
            Health = 0;
        }
        anim.SetBool("atacked", true);
        Invoke("Anim", 0.05f);
        
    }
    public override void Death()
    {
        dead = true;
        GetComponent<Collider2D>().enabled = false;
    }
    public void Anim()
    {
        anim.SetBool("atacked", false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Arrow>())
        {
            RecieveDamage(1);
        }
    }
}
