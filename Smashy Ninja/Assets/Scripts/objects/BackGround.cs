using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
    [SerializeField]
    private float speed, delta, Px, deltaX;

    [SerializeField]
    public bool first;
    //private Rigidbody2D rb;
    [SerializeField]
    public int need;

    private BackGround bg;

    [SerializeField]
    private GameObject NextObject;

    private GameObject Player;

    private string Tag;

    [SerializeField]
    private bool Was = false;

    private void Awake()
    {
        Tag = this.tag;
        //rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }
    public void Update()
    {
        if (!DetectDeltaX())
        {
            transform.position +=new Vector3(0,0,0) * Time.deltaTime;
        }
        else
        {
            if (Time.timeSinceLevelLoad >= 0.2f)
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            }
        }
        if (-(transform.position.x - Player.transform.position.x) > delta && !first)
        {
            PoolScript.instance.ReturnObjectToPool(gameObject);
        }
        SpawnAnother();
        Px = Player.transform.position.x;
    }
    public void SpawnAnother()
    {
        if (transform.position.x + deltaX - Player.transform.position.x < delta && !Was && (need > 0))
        {
            Was = true;
            GameObject S;
            S = PoolScript.instance.GetObjectFromPool(Tag, new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z), Quaternion.identity);
            S.GetComponent<BackGround>().first = false;
            S.GetComponent<BackGround>().need = need - 1;
            S.GetComponent<BackGround>().Was = false;
        }
        if (transform.position.x + deltaX - Player.transform.position.x < delta && !Was && (need == -1))
        {
            Was = true;
            GameObject S;
            S = PoolScript.instance.GetObjectFromPool(Tag, new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z), Quaternion.Euler(0,0,0));
            NextObject = S;
            S.GetComponent<BackGround>().first = false;
            S.GetComponent<BackGround>().need = -1;
            S.GetComponent<BackGround>().Was = false;
            NextObject = S;
        }
    }
    public bool DetectDeltaX()
    {
        return Mathf.Abs(Player.transform.position.x - Px) > 0.01f;
    }
}
