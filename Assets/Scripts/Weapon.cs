using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public TouchButton ShootBtn;
    public GameObject[] Bullets;
    public string MasterName;

    private int BulletNum = 0;
    private bool NewOne = true;


    private void Start() {
        if (MasterName == "") {
            MasterName = gameObject.tag;
        }
        if (ShootBtn) {
            ShootBtn.PointerDownEvent += Shoot;
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            Shoot();
        }
	}

    private void Shoot(object sender,EventArgs e) {
        Shoot();
    }

    private void Shoot() {
        GameObject g = Instantiate(Bullets[BulletNum], transform.position, transform.rotation);
        g.GetComponent<Bullet>().MasterName = MasterName;
        if (NewOne) {
            float CurrentClipLength = g.GetComponent<AudioSource>().clip.length;
            NewOne = false;
            StartCoroutine(RefreshNewOne(CurrentClipLength));
        }
        else {
            g.GetComponent<AudioSource>().enabled = false;
        }
    }

    private IEnumerator RefreshNewOne(float time) {
        yield return new WaitForSeconds(time);
        NewOne = true;
        BulletNum = ++BulletNum % Bullets.Length;
    }
}
