using UnityEngine;
using System.Collections;

public class Bowman : Unit {

    [SerializeField]
    private float rate = 2, atackTime, animTime = 0.8f;
    //private float atackTime;

    private Arrow badArrow;

    private Animator anim;

    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        particle = Resources.Load<GameObject>("Particles/Particle2");
        badArrow = Resources.Load<Arrow>("Particles/badArrow");
    }
    public override void Update()
    {
        if (!dead) {
            if (Health <= 0)
            {
                dead = true;
                Death();
            }
            if (atackTime < rate)
            {
                if (atackTime < animTime * rate)
                {
                    anim.SetBool("atack", false);
                }
                else
                {
                    anim.SetBool("atack", true);
                }
                atackTime += Time.deltaTime;
            }
            else
            {
                Shot();
            }
        }
    }
    public void Shot()
    {
        if (atackTime >= rate)
        {
            FindObjectOfType<AudioManager>().Play("ArrowShot");
            atackTime = 0;
            Vector3 position = transform.position; position.y += 0.9f;
            Arrow newarrow = Instantiate(badArrow, position, badArrow.transform.rotation) as Arrow;
            newarrow.parent = this.gameObject;
        }
    }
    public override void Death()
    {
        anim.SetBool("Dead", true);
        GetComponent<ParticleSystem>().Play(true);
        Invoke("StopBlood", 2f);
        //SpawnParticles(particle, 10);
        //GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Arrow arrow = other.gameObject.GetComponent<Arrow>();
        if (arrow && !arrow.Bad && arrow.parent != gameObject)
        {
            RecieveDamage(1);
        }
    }
}
