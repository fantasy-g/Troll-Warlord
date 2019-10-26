using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] Bullets;

    private int BulletNum = 0;
    private bool NewOne = true;


	void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            Shoot();
        }
	}

    void Shoot() {
        GameObject g = Instantiate(Bullets[BulletNum], transform.position, transform.rotation);
        if (NewOne) {
            float CurrentClipLength = g.GetComponent<AudioSource>().clip.length;
            NewOne = false;
            StartCoroutine(RefreshNewOne(CurrentClipLength));
        }
        else {
            g.GetComponent<AudioSource>().enabled = false;
        }
    }

    IEnumerator RefreshNewOne(float time) {
        yield return new WaitForSeconds(time);
        NewOne = true;
        BulletNum = ++BulletNum % Bullets.Length;
    }
}
