using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float DeadPosY;

	void Start () {
        DeadPosY = GameManager.Instance.deadposy;
	}
	
	void Update () {
        DeadCheck();
	}


    void DeadCheck()
    {
        if(transform.position.y<DeadPosY)
        {
            GameManager.Instance.GameOver();
        }

    }


}
