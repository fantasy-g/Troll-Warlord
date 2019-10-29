using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    public float speed=1;
    private float movespeed;

    public float minPosX;
    public float terPosX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        movespeed = speed * Time.deltaTime;
        transform.Translate(Vector2.left * movespeed, Space.World);
        if(transform.localPosition.x<minPosX)
        {
            transform.localPosition = new Vector2(terPosX, transform.localPosition.y);
        }
	}
}
