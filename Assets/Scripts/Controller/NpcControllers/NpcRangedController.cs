using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcRangedController : NpcController
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartNpc();
        StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp > 0) {
            if ((chasingTarget == false) && (patrolDistance > 0)) {
                Patroling();
            }
            if (ThereIsGround() == true) {
                UpdateChaseRanged();
            }
            TryAttackRanged(npcObject.attackDamage);
        } else {
            if (waitDeathTime <= 0) {
                Destroy(gameObject);
                return;
            } else {
                waitDeathTime -= Time.deltaTime;
            }
        }
    }

    private void TryAttackRanged(int dmg) {
        if (waitAttackTime <= 0) {
            if ((target.position.x > transform.position.x - npcObject.attackRangeX) &&
            (target.position.x < transform.position.x + npcObject.attackRangeX) && 
            (target.position.y > transform.position.y - npcObject.attackRangeY) &&
            (target.position.y < transform.position.y + npcObject.attackRangeY)) {
                Attack();
                GameObject npcAttack = Instantiate(projectile, transform.position, transform.rotation);
                npcAttack.GetComponent<Rigidbody2D>().velocity = (target.position - transform.position).normalized * projectileSpeed;
                npcAttack.gameObject.GetComponent<NpcProjectile>().damage = dmg;
                waitAttackTime = npcObject.attackSpeed;
            }
        } else {
            waitAttackTime -= Time.deltaTime;
        }
    }

    private void UpdateChaseRanged() {
        if (chasingTarget == true) {
            if (Vector2.Distance(transform.position, target.position) <= npcObject.detectionRangeX + 5f) {
                Run();
                ChasingRanged();
            }  else {
                BackToPosition();
            }
        } else {
            SearchPlayer();
        }
    }

    private void ChasingRanged() {
        if (Vector2.Distance(transform.position, target.position) < npcObject.attackRangeX/2) {
            Flip();
            if (ThereIsGround() == true) {
                if (waitHitTime <= 0) {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, -1f *extraSpeed * npcObject.speed * Time.deltaTime);
                } else {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, -1f * (npcObject.speed - knockback) * Time.deltaTime);
                    waitHitTime -= Time.deltaTime;
                }
            }
        } else if (Vector2.Distance(transform.position, target.position) <= npcObject.attackRangeX) {
            Guard();
        } else {
            if (waitHitTime <= 0) {
                transform.position = Vector2.MoveTowards(transform.position, target.position, extraSpeed * npcObject.speed * Time.deltaTime);
            } else {
                transform.position = Vector2.MoveTowards(transform.position, target.position, (npcObject.speed - knockback) * Time.deltaTime);
                waitHitTime -= Time.deltaTime;
            }
        }
        LookToPlayer();
    }
}
