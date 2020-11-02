using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    [SerializeField]
    private float speed = 5;

    private GameObject player;
    private bool dead = false;
    public bool Bad = true, gargoyle = false;

    public GameObject parent;
    private Rigidbody2D rb;
    public Arrow(GameObject newParent)
    {
        parent = newParent;
    }
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (parent)
        {
            if (parent.CompareTag("Player"))
            {
                Bad = false;
                rb.velocity = new Vector3(speed, rb.velocity.y, 0);
            }
            else
            {
                Bad = true;
                rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
            }
            if (parent.GetComponent<Gargoyle>())
            {
                gargoyle = true;
            }
        }
        if (parent) {
            CheckDestroy();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 12) {
            if (other.gameObject != parent && !Bad)
            {
                Destroy(gameObject);
            }
            if (other.gameObject != parent && other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            if (other.gameObject.layer == 8)
            {
                Destroy(gameObject);
            }
            if (other.gameObject != parent && other.gameObject.layer == 10)
            {
                Destroy(gameObject);
            }
        }
    }
    public void CheckDestroy()
    {
        if (Mathf.Abs(parent.transform.position.x - transform.position.x) > 30)
        {
            dead = true;
            Destroy(gameObject);
        }
    }
}
