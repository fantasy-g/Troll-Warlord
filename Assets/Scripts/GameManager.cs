using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public float deadposy = -4.2f;
    //public GameObject replay;
    //public GameObject deadtext;
    public bool PlayerisDead = false;
    public GameObject Player;
   
    private AudioSource audioSource;
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
    void Start ()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
	
	void Update () {
		
	}


    public void GameOver()
    {
        if(PlayerisDead==false)
        {
            PlayerisDead = true;
            Player.SetActive(false);
           
            audioSource.Play();
            float CurrentClipLength =audioSource.clip.length;
            StartCoroutine(RefreshNewOne(CurrentClipLength));
            /*replay.SetActive(true);
            deadtext.SetActive(true);*/
        }
    }
    private IEnumerator RefreshNewOne(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("111");
       Player.transform.position = new Vector3(-7.48f,3f,0);
        Player.SetActive(true);
        PlayerisDead = false;

    }
    public void SetPlayerDead()
    {
        if (PlayerisDead == true)
            PlayerisDead = false;
        Debug.Log("yes");
    }
}
