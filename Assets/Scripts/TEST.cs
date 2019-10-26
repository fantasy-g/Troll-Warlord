using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {
    public Transform MonsterTransform;
    public GameObject MonsterPrefab;
    public TouchButton CreateMonsterBtn;

    private void Start() {
        CreateMonsterBtn.PointerDownEvent += CreateMonster;
    }

    public void CreateMonster(object sender,EventArgs e) {
        GameObject go =  Instantiate(MonsterPrefab, MonsterTransform);
    }
}
