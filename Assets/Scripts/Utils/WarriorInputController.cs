using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorInputController : MonoBehaviour
{
    public Animator animatorPlayer;
    public PlayerMovement player;
    public float runSpeed = 40f;
    public PlayerActions playerActions;
    public float stompSpeed = 100;
    private Rigidbody2D rb;
    private float RightLastTapTime;
    private float LeftLastTapTime;
    public int stompDamage;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool canStomp = true;
    bool isStomping = false;

    bool attack = false;


    void Start()
    {
        //Class = ClassActionsFactory.Create();
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
    }


    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animatorPlayer.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animatorPlayer.SetBool("isJumping", true);
        }

        // if (Input.GetButtonDown("Crouch"))
        // {
        //     crouch = true;
        // }
        // else if (Input.GetButtonUp("Crouch"))
        // {
        //     crouch = false;
        // }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack = true;
            //animatorPlayer.SetTrigger("Attack");
            playerActions.Attack();
        }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     animatorPlayer.SetTrigger("Death");
        // }

        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     animatorPlayer.SetTrigger("Hit");
        // }

        /* if(direction == 0){
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                if(Time.time - RightLastTapTime < tapSpeed){
                    direction = 1;              //direita
                }
                RightLastTapTime = Time.time;
            } else if(Input.GetKeyDown(KeyCode.LeftArrow)){
                if(Time.time - LeftLastTapTime < tapSpeed){
                    direction = 2;              //esquerda
                }
                LeftLastTapTime = Time.time;
            }
        } else {
            if(dashTime <= 0){
                direction = 0;
                dashTime = dashStartTime;
                rb.velocity = Vector2.zero;
            } else {
                dashTime = dashTime - Time.deltaTime;
                playerActions.SpecialMovement(direction, dashSpeed);
            }
        } */

        if(player.getGrounded()){
            canStomp = true;
            isStomping = false;
        }

        if (Input.GetButtonDown("SMove"))
        {
            if((!player.getGrounded()) && canStomp)
            {
                playerActions.SpecialMovement(0, stompSpeed);
                isStomping = true;
                canStomp = false;
            }
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
        animatorPlayer.SetBool("isJumping", false);
        ResetAttack();
    }

    public void stompDetected(Collider2D col)
    {
        if(isStomping && col.gameObject.tag == "NPC")
        {
            col.gameObject.GetComponent<NpcController>().Hit(stompDamage);
            playerActions.rigidBody.velocity = new Vector2(playerActions.rigidBody.velocity.x, stompSpeed/4);
            isStomping = false;
        }
    }

}