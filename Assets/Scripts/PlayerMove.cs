﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public Joystick joystick;
    public TouchButton SpaceBtn;
    public float Speed = 7;

    private new Rigidbody2D rigidbody;
    private Animator animator;


    #region [Ground Check]
    [Header("Ground Check")]
    public LayerMask GroundLayerMask;
    public Transform FootPos;
    public float GroundCheckRadius = 0.2f;
    private bool isGrounded = false;
    #endregion


    #region [Jump]
    [Header("Jump")]
    public float JumpForce = 25;
    public float JumpTime = .35f;
    public int JumpTimes = 2;

    private int jumpTimes = 0;
    private float jumpTimer = 0;
    private float FallForce;    // JumpForce 的1.5倍
    private float fallForce;
    private bool isJumping = false;
    private bool hasAccFallSpeed = false;
    #endregion

    [Header("Conf")]
    public bool EnableDecreaseInJumpUp = true;
    public float FallForceMulti = 1.2f;
    public float FallLerpTime = .08f;
    public float DecreaseThresholdInJumpUp = 0.5f;  // 1->0
    

    void Start () {
        if (SpaceBtn != null) {
            SpaceBtn.PointerDownEvent += Jump;
            SpaceBtn.PointerUpEvent += JumpEnd;
        }

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        FallForce = FallForceMulti * JumpForce;

         
    }
	
	void Update () {
        InputCheck();       // PC 端键盘输入事件
        Move();
        GroundCheck();      // 先检测是否 isGrounded
        JumpCheck();        // Jump 检测中用到 isGrounded
	}


    private void JumpCheck() {
        // 用最大跳跃时间限制高度
        if (jumpTimer < 0) {
            JumpEnd(this, EventArgs.Empty);
        }
        else if (jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
        }

        // 加速下落
        if (rigidbody.velocity.y < 0 && !hasAccFallSpeed) {
            fallForce = 0;
            FallForce = FallForceMulti * JumpForce;
            hasAccFallSpeed = true;
        }
        // 正在下落 速度控制
        if (hasAccFallSpeed) {
            fallForce = Mathf.Lerp(fallForce, FallForce, FallLerpTime);     // TEST_LerpTime = .12f
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -fallForce);
        }

        // 上升时的减速
        if (EnableDecreaseInJumpUp && isJumping && !hasAccFallSpeed) {
            if (jumpTimer / JumpTime < DecreaseThresholdInJumpUp)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, (jumpTimer / JumpTime) * JumpForce);
        }

        // 下落后落到地面
        if (isGrounded && hasAccFallSpeed) {
            isJumping = false;
            jumpTimes = 0;
            hasAccFallSpeed = false;
        }
    }

    private void InputCheck() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isJumping || (jumpTimes < JumpTimes)) {
                Jump(this, EventArgs.Empty);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            JumpEnd(this, EventArgs.Empty);
        }
    }

    private void Move() {
        float h = joystick.Horizontal();
        if (h != 0) Flip(h < 0);
        rigidbody.velocity = new Vector2(h * Speed, rigidbody.velocity.y);

        // Animator [Walk & Idle]
        if (isJumping || animator == null) {
            return;
        }
        else if (h != 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
            animator.Play("Walk");
        }
        else if (h == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            animator.Play("Idle");
        }
        animator.SetFloat("Speed", Mathf.Max(0.2f, Mathf.Abs(h)));
    }

    private void Jump(object sender, EventArgs e) {
        if (jumpTimes >= JumpTimes)
            return;
        if (animator != null)
            animator.Play("Jump");
        isJumping = true;
        hasAccFallSpeed = false;
        jumpTimer = JumpTime;
        jumpTimes++;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpForce);
    }

    private void JumpEnd(object sender, EventArgs e) {
        jumpTimer = 0;
        if (!hasAccFallSpeed)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
    }

    private void GroundCheck() {
        isGrounded = Physics2D.OverlapCircle(FootPos.position, GroundCheckRadius, GroundLayerMask);
    }

    private void Flip(bool left) {
        if ((left && transform.right.x < 0) || (!left && transform.right.x > 0))
            return;
        transform.Rotate(new Vector3(0, 180));
    }
}
