using UnityEngine;
using System.Collections;

public class CloudGen : MonoBehaviour {
    [SerializeField]
    private float maxHeight = 27.5f;

    [SerializeField]
    private float dY;

    private GameObject Player;

    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update () {
        if (Player.transform.position.y + dY >= maxHeight)
        {
            SpownNew();
        }
        if (Player.transform.position.y + dY + 2 < maxHeight && maxHeight >= 29.5f)
        {
            maxHeight -= 2f;
        }
	}
    private void SpownNew()
    {
        GameObject cloud = PoolScript.instance.GetObjectFromPool("Cloud", new Vector3(Player.transform.position.x + Random.Range(-3f, 3f), maxHeight + 2f, 0), Quaternion.Euler(Vector3.zero));
        maxHeight += 2f;
    }
}
