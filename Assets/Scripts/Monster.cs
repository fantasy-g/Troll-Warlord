using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    
    public int Damage = 100;
    public int Bonus = 100;

    public float Speed = 5;
    protected float speed = 0;
    protected Vector3 direction = new Vector3(1, 0);


    void Start() {
        speed = Speed;
    }

    void FixedUpdate() {
        Move();
    }

    // 碰撞
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") {
            MoveBack(collision.collider);
        }
        else {
            // GameManager.Instance.Player.Hurt(Damage);
        }
    }
    // 空气墙碰撞
    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "AirWall") {
            bool leftDir = collision.transform.position.x > transform.position.x;
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

    public IEnumerator Die() {
        speed = 0;

        // 死亡效果、死亡动画
        // ... Fade 效果

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
