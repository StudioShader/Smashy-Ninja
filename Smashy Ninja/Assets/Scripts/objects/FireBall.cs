using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
    [SerializeField]
    GameObject parent;

    [SerializeField]
    public float dY = 1, angle, dA, maxDistanse;

    public void Awake()
    {
        parent = transform.parent.gameObject;
        maxDistanse = Mathf.Sqrt((transform.position.x - (parent.transform.position.x)) * (transform.position.x - (parent.transform.position.x)) + (transform.position.y - (parent.transform.position.y + dY)) * (transform.position.y - (parent.transform.position.y + dY)));
        angle = Mathf.Atan2((transform.position.y - (parent.transform.position.y + dY)), (transform.position.x - (parent.transform.position.x)));
    }

    public void Update()
    {
        angle += dA * Time.deltaTime;
        transform.localPosition = new Vector2(Mathf.Cos(angle) * maxDistanse,dY + Mathf.Sin(angle) * maxDistanse);
    }
}
