using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float speed;

    private float horizontalMove = 0.0f;
    private bool jump = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            SoundManager.S.MakeJumpSound();
            jump = true;
            animator.SetBool("isOnGround", false);
        }

        // Get approx direction
        float ourSpeed = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(ourSpeed));
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void PlayerLanded()
    {
        animator.SetBool("isOnGround", true);
    }
}
