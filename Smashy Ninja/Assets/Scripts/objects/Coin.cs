using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    Rigidbody2D rb;

    private GameObject targ, Player;

    private Vector3 target;

    public bool dead = false;
        
	void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
        targ = GameObject.FindGameObjectWithTag("CoinTarget");
        Invoke("Destr",3f);
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        target = targ.transform.position + Player.transform.position;
        if (dead)
        {
            transform.position = transform.position + 0.1f * (target - transform.position);
        }
        Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - gameObject.GetComponent<CircleCollider2D>().radius, transform.position.y - 0.4f), new Vector2(transform.position.x + gameObject.GetComponent<CircleCollider2D>().radius, transform.position.y));
        for (int i=0;i<colliders.Length;i++)
        {
            if (colliders[i].gameObject.layer == 16 || colliders[i].gameObject.layer == 17 || colliders[i].gameObject.layer == 8)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }
    public void Del()
    {
        Invoke("Destr", 0.5f);
        GetComponent<Collider2D>().enabled = false;
        dead = true;
    }
    private void Destr()
    {
        Destroy(gameObject);
    }
}
