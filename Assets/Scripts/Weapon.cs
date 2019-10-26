using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject[] Bullets;
    public GameObject Special;
    public string MasterName;
    public Slider slider;
    private int BulletNum = 0;
    private bool NewOne = true;
    public float LoseTimer = 0;
    private Color color;
    private void Start() {
        if (MasterName == "") {
            MasterName = gameObject.tag;
        }

        color = slider.fillRect.transform.GetComponent<Image>().color;
    }

    void Update ()
    {
        if (slider.value < 100)
        {
            LoseTimer += Time.deltaTime;
            if (LoseTimer > 1)
            {
                LosePower();
                LoseTimer = 0;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Shoot();
                slider.value += 10;
            }
        }
        else
        {
            slider.fillRect.transform.GetComponent<Image>().color = Color.red;
            if (Input.GetKeyDown(KeyCode.F))
            {
                ShootSpecial();
            }
        }

    }

    private void Shoot() {
       
        
            GameObject g = Instantiate(Bullets[BulletNum], transform.position, transform.rotation);
            g.GetComponent<Bullet>().MasterName = MasterName;
            if (NewOne)
            {
                slider.value += 20;
                float CurrentClipLength = g.GetComponent<AudioSource>().clip.length - 0.25f;
                NewOne = false;
                StartCoroutine(RefreshNewOne(CurrentClipLength));
            }
            else
            {
                g.GetComponent<AudioSource>().enabled = false;
            }
        
        
    }

    void ShootSpecial()
    {
        GameObject g = Instantiate(Special, transform.position, transform.rotation);
        g.GetComponent<Bullet>().MasterName = MasterName;
        if (NewOne)
        {
            slider.value += 20;
            float CurrentClipLength = g.GetComponent<AudioSource>().clip.length - 0.25f;
            NewOne = false;
            StartCoroutine(RefreshNewOne1(CurrentClipLength));
        }
        else
        {
            g.GetComponent<AudioSource>().enabled = false;
        }
    }
    private IEnumerator RefreshNewOne(float time) {
        yield return new WaitForSeconds(time);
        NewOne = true;
        BulletNum = ++BulletNum % Bullets.Length;
        
    }
    private IEnumerator RefreshNewOne1(float time)
    {
        yield return new WaitForSeconds(time);
        NewOne = true;
        BulletNum = ++BulletNum % Bullets.Length;
        slider.value = 0;
        slider.fillRect.transform.GetComponent<Image>().color = color;
    }


    private void LosePower()
    {
        if (slider.value > 0)
        {
            slider.value -= 5;
        }

        if (slider.value < 0)
        {
            slider.value = 0;
        }

       
    }
}
