using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageActions : ClassActions
{
    private GameObject iceball;

    private void Start()
    {
        this.iceball = Resources.Load("Prefabs/Iceball") as GameObject;
    }
    override public void Attack()
    {
        animator.SetTrigger("Attack");
        Instantiate(iceball, attackPos.position, attackPos.rotation).GetComponent<Iceball>().damage = character.AttackDamage;
    }


    override public void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    override public void SpecialMovement(int direction, float speed)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
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
