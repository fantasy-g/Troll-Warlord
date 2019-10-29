using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public float deadposy = -4.2f;
    public GameObject replay;
    public GameObject deadtext;
    public bool PlayerisDead = false;


    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            return;
        }
    }
    void Start () {
	}
	
	void Update () {
		
	}


    public void GameOver()
    {
        if(PlayerisDead==false)
        {
            PlayerisDead = true;
            replay.SetActive(true);
            deadtext.SetActive(true);
        }
    }

    public void SetPlayerDead()
    {
        if (PlayerisDead == true)
            PlayerisDead = false;
        Debug.Log("yes");
    }
}
