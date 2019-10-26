using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float Speed = 10;
    public string MasterName;


    void Start() {
        GetComponent<Rigidbody2D>().velocity = Speed * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Bullet broBullet = collision.gameObject.GetComponent<Bullet>();
        if (broBullet) {
            if (broBullet.MasterName == MasterName)
                return;
            else
                Die();
        }

        if (collision.tag != MasterName) {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != MasterName)
            Die();
    }

    public void Die() {
        StartCoroutine(_Die());
    }

    private IEnumerator _Die() {
        float delay = 0;
        AudioSource audio = GetComponent<AudioSource>();
        if (audio && audio.enabled == true) {
            delay = audio.clip.length;
        }
        DieEffect();
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);    // temp
    }

    private void DieEffect() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
            renderer.enabled = false;
        }
    }
}
