using UnityEngine;
using System.Collections;

public class WoodBoard : MonoBehaviour {
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float DX, DY;
    [SerializeField]
    private float dTime, deltaTime = 0.1f;
    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        if (!Player.GetComponent<Player>().dead)
        {
            if (Player.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                gameObject.layer = 17;
            }
            else
            {
                gameObject.layer = 16;
            }
            if (CheckDown())
            {
                gameObject.layer = 16;
            }
        }
        else
        {
            gameObject.layer = 17;
        }
    }
    public bool CheckDown()
    {
        if (Player.GetComponent<Player>().Down)
        {
            return true;
        }
        return false;
    }
}
