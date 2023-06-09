﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public Animator animatorPlayer;
    public PlayerMovement player;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    bool attack = false;
    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animatorPlayer.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animatorPlayer.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack = true;
            animatorPlayer.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animatorPlayer.SetTrigger("Death");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            animatorPlayer.SetTrigger("Hit");
        }


    }

    private void HandleAttack()
    {
        if (attack)
        {

        }
    }

    public void onCrouching(bool isCrouching)
    {
        animatorPlayer.SetBool("isCrouching", isCrouching);
    }

    public void onLanding()
    {
        animatorPlayer.SetBool("isJumping", false);
    }

    private void ResetAttack()
    {
        attack = false;
    }

    void FixedUpdate()
    {
        // Move our character
        player.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        ResetAttack();
    }
}