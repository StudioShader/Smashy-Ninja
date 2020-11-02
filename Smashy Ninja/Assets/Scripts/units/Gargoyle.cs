using UnityEngine;
using System.Collections;

public class Gargoyle : Unit {
    [SerializeField]
    private float rate = 2, atackTime, atack, animTime = 0.8f;
    //private float atackTime;
    private Animator anim;

    private Arrow badBall;

    public void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        particle = Resources.Load<GameObject>("Particles/Particle2");
        badBall = Resources.Load<Arrow>("Particles/BadBall");
    }
    public override void Update()
    {
        if (!dead)
        {
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
        else
        {
            if (isGrounded())
            {
                anim.SetBool("Grounded", true);
            }
        }
    }
    public void Shot()
    {
        FindObjectOfType<AudioManager>().Play("Ball");
        atackTime = 0;
        Vector3 position = transform.position; position.y += 1.4f;
        Arrow newball = Instantiate(badBall, position, badBall.transform.rotation) as Arrow;
        newball.parent = this.gameObject;

    }
    public bool isGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].gameObject.GetComponent<Collider2D>().isTrigger && colliders[i].gameObject != gameObject && colliders[i].gameObject.layer != 12 && colliders[i].gameObject.layer != 13 && colliders[i].gameObject.layer != 9)
            {
                return true;
            }
        }
        return false;
    }
    public override void Death()
    {
        gameObject.layer = 14;
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        anim.SetBool("Dead", true);
        Invoke("StopBlood", 2f);
        //SpawnParticles(particle, 4);
        //GetComponentInChildren<SpriteRenderer>().enabled = false;
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
