using UnityEngine;
using System.Collections;

public class Barrel : Unit {

    [SerializeField]
    private float t;

    public void Awake()
    {
        particle = Resources.Load<GameObject>("Particles/Particle1");
    }
    public override void Update()
    {
        if (Health <= 0 && !dead)
        {
            Death();
            dead = true;
        }
    }
    public override void Death()
    {
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, t);
        SpawnParticles(particle,18);
        Explosion();
    }
    public override void SpawnParticles(GameObject prefab, int n)
    {
        for(int i = 0; i < n; i++)
        {
            GameObject B = Instantiate(prefab, new Vector3(transform.position.x ,transform.position.y + 0.5f), transform.rotation) as GameObject;
            B.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1.5f,1.5f), Random.Range(1f, 3f)),ForceMode2D.Impulse);
        }
    }
    public void Explosion()
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + 1), 6);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Unit>())
            {
                colliders[i].GetComponent<Unit>().RecieveDamage(1);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Arrow>() && !collision.gameObject.GetComponent<Arrow>().Bad)
        {
            RecieveDamage(1);
        }
    }
}
