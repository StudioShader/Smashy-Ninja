using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    [SerializeField]
    public float Health;

    public Rigidbody2D rb;

    public bool dead = false;

    public GameObject particle;

    public virtual void Update()
    {
        
    }
    public virtual void RecieveDamage(float damage)
    {
        if (Health >= damage)
        {
            Health -= damage;
        }
        else
        {
            Health = 0;
        }
    }
    public virtual void Death()
    {
    }
    public virtual void Atack()
    {
    }
    public virtual void Abbility()
    {

    }
    public virtual void SpawnParticles(GameObject prefab, int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject B = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + 0.5f), transform.rotation) as GameObject;
            B.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.5f, 1f)), ForceMode2D.Impulse);
        }
    }
    public virtual void StopBlood()
    {
        GetComponent<ParticleSystem>().Stop(true);
    }
    public virtual void Del()
    {

    }

}
