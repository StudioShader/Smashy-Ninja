using UnityEngine;
using System.Collections;

public class Chest : Unit {

    private GameObject coin;

    private Animator anim;

    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        coin = Resources.Load<GameObject>("Particles/coin");
        Health = 1;
    }

    public override void Update () {
	    
	}
    public override void RecieveDamage(float damage)
    {
        anim.SetBool("Open", true);
        if (Health - damage >= 0) {
            FindObjectOfType<AudioManager>().Play("CoinDrop");
            SpawnParticles(coin, 7);
            Health -= damage;
        }
        else
        {
            Health = 0;
        }
        
    }
}
