using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] Bullet;
    public int BulletNum = 0;
    public GameObject weaponRight;
    public GameObject weaponLeft;
   
	void Start () {
		
	}
	
	void Update () {
		Shoot();
        SetBullet();
	}

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (weaponRight.transform.position.x > weaponLeft.transform.position.x)
            {
                GameObject g = Instantiate(Bullet[BulletNum], weaponRight.transform.position, Bullet[BulletNum].transform.rotation);
                BulletNum++;
            }
            else
            {
               
               GameObject g= Instantiate(Bullet[BulletNum], weaponRight.transform.position, Bullet[BulletNum].transform.rotation);
                //g.transform.rotation = new Quaternion(g.transform.rotation.x, g.transform.rotation.y ,g.transform.rotation.z+90, 0);
                BulletNum++;

            }

            if (BulletNum == 7)
            {
                BulletNum = 0;
            }
        }
    }

    void SetBullet()
    {
        if (weaponRight.transform.position.x > weaponLeft.transform.position.x)
        {
            Bullet[BulletNum].GetComponent<Bullet>().t = -1;
           
            Debug.Log("you");
        }
        else
        {
           Bullet[BulletNum].GetComponent<Bullet>().t = 1;
            Debug.Log("zuo");
        }
    }
}
