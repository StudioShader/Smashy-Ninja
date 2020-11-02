using UnityEngine;
using System.Collections;

public class Deletable : MonoBehaviour {

    private GameObject Player;

    [SerializeField]
    private float dX, dY;

    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (transform.position.x + dX <= Player.transform.position.x || transform.position.y + dY <= Player.transform.position.y || transform.position.y - dY >= Player.transform.position.y || transform.position.x - dX >= Player.transform.position.x)
        {
            PoolScript.instance.ReturnObjectToPool(gameObject);
        }
    }
}
