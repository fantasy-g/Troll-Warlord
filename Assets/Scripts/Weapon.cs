using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Slider slider;
    public TouchButton ShootBtn;
    public GameObject[] Bullets;
    public GameObject SpecialPrefab;
    public string MasterName;

    public float PowerDecreaseSpeed = 5f;
    public Color SpecialSliderColor;
    public bool AllowBulletFollowSpecial = false;

    private int BulletNum = 0;
    private bool NewOne = true;
    private Color rawSliderColor;
    private bool special = false;


    private float PowerValue {
        get { return slider.value; }
        set {
            if (slider.value == 100 && value != 100) {
                slider.fillRect.transform.GetComponent<Image>().color = rawSliderColor;
            }
            slider.value = Math.Min(value, 100);
            slider.value = Math.Max(slider.value, 0);
            if (slider.value == 100) {
                slider.fillRect.transform.GetComponent<Image>().color = SpecialSliderColor;
            }
        }
    }



    private void Start() {
        if (MasterName == "") {
            MasterName = gameObject.tag;
        }
        if (ShootBtn) {
            ShootBtn.PointerDownEvent += Shoot;
        }
        if (slider) {
            rawSliderColor = slider.fillRect.transform.GetComponent<Image>().color;
        }
        if (SpecialSliderColor == null) {
            SpecialSliderColor = Color.red;
        }
    }

    void Update() {
        if (PowerValue < 100) {
            PowerValue -= PowerDecreaseSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            Shoot(PowerValue >= 100);
        }
    }


    private void Shoot(object sender,EventArgs e) {
        Shoot(PowerValue >= 100);
    }

    private void Shoot(bool _special = false) {
        if (!AllowBulletFollowSpecial && _special == false && special == true)
            return;     // 刚发射了特殊子弹的时段
        special = _special;

        PowerValue += 10;
        GameObject prefab = special ? SpecialPrefab : Bullets[BulletNum];
        GameObject go = Instantiate(prefab, transform.position, transform.rotation);
        go.GetComponent<Bullet>().MasterName = MasterName;

        if (special) {
            PowerValue = 0;
            if (AllowBulletFollowSpecial) {
                StartCoroutine(RefreshNewOne(0));
            }
            else {
                float CurrentClipLength = go.GetComponent<AudioSource>().clip.length;
                StartCoroutine(RefreshNewOne(CurrentClipLength));
                StartCoroutine(RefreshSpecial(CurrentClipLength));
            }
            return;
        }

        if (NewOne) {
            float CurrentClipLength = go.GetComponent<AudioSource>().clip.length;
            NewOne = false;
            StartCoroutine(RefreshNewOne(CurrentClipLength));
        }
        else {
            go.GetComponent<AudioSource>().enabled = false;
        }
    }

    private IEnumerator RefreshNewOne(float time) {
        yield return new WaitForSeconds(time);
        NewOne = true;
        BulletNum = ++BulletNum % Bullets.Length;
    }

    private IEnumerator RefreshSpecial(float time) {
        yield return new WaitForSeconds(time);
        special = false;
    }
    
}
