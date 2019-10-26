using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public int t = 0;
    private Rigidbody rigidbody;
    
	void Start ()
	{
	    rigidbody = this.GetComponent<Rigidbody>();
	}
	
	void Update () {

	    if (t > 0)
	    {
            rigidbody.velocity=new Vector3(-10,0,0);
	    }
	    else
	    {
	        rigidbody.velocity=new Vector3(10,0,0);
	    }
	 
	 
	}
}
