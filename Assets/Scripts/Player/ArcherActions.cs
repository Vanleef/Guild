using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherActions : ClassActions
{
    private GameObject arrow;
    float dashDelay = 1.0f;
    float lastDash = -9999f;
    float dodgeTime = 0.25f;


    void Start()
    {
        arrow = Resources.Load("Prefabs/Arrow") as GameObject;
    }

    void Update()
    {
        if(Time.time < (lastDash + dodgeTime)){
            isDodging = true;
            Physics2D.IgnoreLayerCollision(11, 10, true);
            Physics2D.IgnoreLayerCollision(11, 12, true);
        } else {
            isDodging = false;
            Physics2D.IgnoreLayerCollision(11, 10, false);
            Physics2D.IgnoreLayerCollision(11, 12, false);
            animator.SetBool("isDodging", false);
        }
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack");
        Instantiate(arrow, attackPos.position, attackPos.rotation).GetComponent<Arrow>().damage = character.AttackDamage;
    }

    public override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialMovement(int direction, float speed)
    {
        if (Time.time > (lastDash + dashDelay))
        {
            if (direction == 1)
            {
                rigidBody.velocity = Vector2.right * speed;
                animator.SetBool("isDodging", true);
            }
            else if (direction == 2)
            {
                rigidBody.velocity = Vector2.left * speed;
                animator.SetBool("isDodging", true);
            }
            lastDash = Time.time;
        }
    }

    override public void GetHit()
    {
        animator.SetTrigger("Hit");
    }

    override public void Die()
    {
        animator.SetTrigger("Death");
    }

}
