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
        if (collision.tag == "Bullet") {
            Bullet broBullet = collision.gameObject.GetComponent<Bullet>();
            if (broBullet) {
                if (broBullet.MasterName != MasterName) {
                    Die();
                }
                return;
            }
        }
        else if (collision.tag != MasterName) {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != MasterName) {
            Die();
        }
    }

    public void Die() {
        StartCoroutine(_Die());
    }


    private bool dead = false;
    private IEnumerator _Die() {
        if (dead) yield break;
        dead = true;

        float delay = 0;
        AudioSource audio = GetComponent<AudioSource>();
        if (audio && audio.enabled == true) {
            delay = audio.clip.length;
        }

        float effectDelay = DieEffect();
        delay = Mathf.Max(effectDelay, delay);
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);    // temp
    }

    private float DieEffect() {
        float delay = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
            renderer.enabled = false;
        }

        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        if (particle) {
            particle.Play();
            delay = Mathf.Max(particle.main.duration, delay);
        }
        return delay;
    }
}
