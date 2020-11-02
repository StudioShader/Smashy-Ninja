using UnityEngine;
using System.Collections;

public class ShurikenSprite : MonoBehaviour {
    [SerializeField]
    private float angle;
    public void Update()
    {
        transform.Rotate(new Vector3(0, 0, transform.rotation.z + angle));
    }
}
