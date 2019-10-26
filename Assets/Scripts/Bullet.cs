using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10;

    void Start() {
        GetComponent<Rigidbody>().velocity = Speed * transform.right;
    }
}
