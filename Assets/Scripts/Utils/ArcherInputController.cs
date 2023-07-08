using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherInputController : MonoBehaviour
{
    public Animator animatorPlayer;
    public PlayerMovement player;
    public float runSpeed = 40f;
    public float dashSpeed = 70f;
    [SerializeField]
    private PlayerActions playerActions;
    //private IClassActions Class;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool hasDoubleJump = true;

    bool attack = false;


    void Start()
    {
        //Class = ClassActionsFactory.Create();
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
            //Class.Attack();
        }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     animatorPlayer.SetTrigger("Death");
        // }

        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     animatorPlayer.SetTrigger("Hit");
        // }

        if(Input.GetButtonDown("SMove"))
        {
            if(player.getIsFacingRight()){
                playerActions.SpecialMovement(2, dashSpeed);
            } else {
                playerActions.SpecialMovement(1, dashSpeed);
            }
            
        }

    }

    public void onCrouching(bool isCrouching)
    {
        animatorPlayer.SetBool("isCrouching", isCrouching);
    }

    public void onLanding()
    {
        Debug.Log("landing");
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
}