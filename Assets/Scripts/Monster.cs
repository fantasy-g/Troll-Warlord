using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    
    public int Damage = 100;
    public int Bonus = 100;

    public float Speed = 5;
    protected float speed = 0;
    protected Vector3 direction = new Vector3(1, 0);
    protected bool CollisonEnable = true;


    void Start() {
        speed = Speed;
    }

    void FixedUpdate() {
        Move();
    }

    // 碰撞
    private void OnCollisionEnter2D(Collision2D collision) {
        if (!CollisonEnable) return;
        if (collision.gameObject.tag != "Player") {
            MoveBack(collision.collider);
        }
        else {
            // GameManager.Instance.Player.Hurt(Damage);
            Die();      /// test
        }
    }
    
    protected void OnTriggerEnter2D(Collider2D collision) {
        if (!CollisonEnable) return;
        if (collision.gameObject.tag == "AirWall") {
            MoveBack(collision);
        }
    }

    protected void Move() {
        transform.position += direction * Time.deltaTime * speed;
    }

    protected void MoveBack(Collider2D collision) {
        // 运动方向翻转
        bool left = collision.transform.position.x > transform.position.x;
        direction = left ? new Vector3(-1, 0) : new Vector3(1, 0);

        // 人物翻转
        Vector3 scale = transform.localScale;
        scale.x *= (left && scale.x > 0) || (!left && scale.x < 0) ? -1 : 1;
        transform.localScale = scale;
    }


    protected float waitForDieEffectTime = 0f;

    public void Die() {
        StartCoroutine(_Die());
    }

    protected void DieEffect() {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim) {
            anim.Play("Die");
            waitForDieEffectTime = Mathf.Max(waitForDieEffectTime, anim.GetCurrentAnimatorStateInfo(0).length);
        }

        ParticleSystem particle = transform.GetComponentInChildren<ParticleSystem>();
        if (particle) {
            particle.Play();
            waitForDieEffectTime = Mathf.Max(waitForDieEffectTime, particle.main.duration);
        }
    }

    private IEnumerator _Die() {
        speed = 0;
        CollisonEnable = false;
        DieEffect();
        yield return new WaitForSeconds(waitForDieEffectTime);
        Destroy(gameObject);
    }

}
