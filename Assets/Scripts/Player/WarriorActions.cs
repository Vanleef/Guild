using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorActions : ClassActions
{

    override public void Attack()
    {
        animator.SetTrigger("Attack");
        //particles.Play();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, character.attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if(enemiesToDamage[i].GetComponent<Spider>() != null){
                Debug.Log("Encontrei spider boss");
                enemiesToDamage[i].GetComponent<Spider>().Hit(character.AttackDamage);
            }else{
                Debug.Log("Encontrei npc");
                enemiesToDamage[i].GetComponent<NpcController>().Hit(character.AttackDamage);
            }
        }
    }

    override public void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    override public void SpecialMovement(int direction, float stompSpeed)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, -stompSpeed);
        //rigidBody.AddForce(new Vector2(0, speed));
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
