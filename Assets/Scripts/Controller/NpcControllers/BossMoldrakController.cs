using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoldrakController : NpcController
{
    private float waitNextFormTime;         // Time to enter on second form
    private float waitSpecialAttackTime;    // Time to use the special attack again
    private Vector2[] specialAttackPoints; // Points to go to use special attack
    private Rigidbody2D rigidBody;
    public float distanceToJump;            // Distance from player
    private bool isGrounded;
    public float jumpForce;
    public int extraDmg;                    // Extra damage when enter on second form
    public float extraDashSpeed;            // Extra speed when using special attack
    private bool isSecondForm = false;
    private int specialAttackStage;
    private int currentDamage;
    public float despawnDistance;
    private bool checkBossSound = false;
    
    void Start()
    {
        StartBossMusic();
        StartNpc();
        waitNextFormTime = 3f;
        waitSpecialAttackTime = Random.Range(8f, 10f);
        specialAttackStage = 1;
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        isGrounded = true;
        specialAttackPoints = new Vector2[] {
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x - 52, transform.position.y)
        };
        currentDamage = npcObject.attackDamage;
    }

    void Update()
    {
        if (currentHp > 0) {
            if (Vector2.Distance(transform.position, target.position) > despawnDistance) {
                Destroy(gameObject);
                return;
            }
            if (isSecondForm == false) {
                FistFormBehavior();
            } else {
                SecondFormBehavior();
            }
        } else {
            Die();
            Destroy(transform.gameObject.GetComponent<Collider2D>());
            Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
            currentDamage -= extraDmg;
            if(gameObject.GetComponent<UnlockController>() != null){
                gameObject.GetComponent<UnlockController>().unlockProgress(gameObject.GetComponent<UnlockController>().unlockOnDeath);
            }
            return;
        }
    }

    private void FistFormBehavior() {
        if (currentHp > npcObject.hp/2) {
            if (waitSpecialAttackTime > 0) {
                LookToPlayer();
                Walk();
                if (Vector2.Distance(transform.position, target.position) > distanceToJump) {
                    /*if (isGrounded == true) {
                        Jump();
                        rigidBody.AddForce(new Vector2(jumpForce/2, jumpForce));
                        isGrounded = false;
                    }*/
                    transform.position = Vector2.MoveTowards(transform.position, target.position, 1.5f * npcObject.speed * Time.deltaTime);
                } else {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, npcObject.speed * Time.deltaTime);
                    isGrounded = true;
                }
                TryAttack(currentDamage);
                waitSpecialAttackTime -= Time.deltaTime;
            } else {
                if (specialAttackStage != 0) {
                    SpecialAttack();
                } else {
                    waitSpecialAttackTime = Random.Range(8f, 10f);
                    specialAttackStage = 1;
                }
            }
        } else {
            if (waitNextFormTime == 3f) {
                Transformation();
                Physics2D.IgnoreLayerCollision(12, 11, true);
            }
            if (waitNextFormTime <= 0) {
                Physics2D.IgnoreLayerCollision(12, 11, false);
                isSecondForm = true;
            } else {
                waitNextFormTime -= Time.deltaTime;
            }
        }
    }

    private void SecondFormBehavior() {
        if (waitSpecialAttackTime > 0) {
            LookToPlayer();
            Walk();
            if (Vector2.Distance(transform.position, target.position) > distanceToJump) {
                /*if (isGrounded == true) {
                    Jump();
                    rigidBody.AddForce(new Vector2(jumpForce/2, jumpForce));
                    isGrounded = false;
                }*/
                transform.position = Vector2.MoveTowards(transform.position, target.position, 2f * npcObject.speed * Time.deltaTime);
            } else {
                transform.position = Vector2.MoveTowards(transform.position, target.position, npcObject.speed * Time.deltaTime);
                isGrounded = true;
            }
            TryAttack(currentDamage);
            waitSpecialAttackTime -= Time.deltaTime;
        } else {
            if (specialAttackStage != 0) {
                SpecialAttack();
            } else {
                waitSpecialAttackTime = Random.Range(5f, 8f);
                specialAttackStage = 1;
            }
        }
    }

    private void SpecialAttack() {
        Physics2D.IgnoreCollision(transform.gameObject.GetComponent<Collider2D>(), target.gameObject.GetComponent<Collider2D>());
        //Physics2D.IgnoreLayerCollision(12, 11, true);
        switch (specialAttackStage) {
            case 1:
                if (facingRight == false) {
                    Flip();
                }
                if (Vector2.Distance(transform.position, specialAttackPoints[0]) < 0.2f) {
                    specialAttackStage = 2;
                } else {
                    Walk();
                    transform.position = Vector2.MoveTowards(transform.position, specialAttackPoints[0], 1.5f * npcObject.speed * Time.deltaTime);
                }
                break;
            case 2:
                if (facingRight == true) {
                    Flip();
                }
                if (Vector2.Distance(transform.position, specialAttackPoints[1]) < 0.2f) {
                    if (isSecondForm == true) {
                        specialAttackStage = 3;
                    } else {
                        specialAttackStage = 999;
                    }
                } else {
                    Run();
                    transform.position = Vector2.MoveTowards(transform.position, specialAttackPoints[1], extraDashSpeed * npcObject.speed * Time.deltaTime);
                    TryAttack(currentDamage);
                }
                break;
            case 3:
                if (facingRight == false) {
                    Flip();
                }
                if (Vector2.Distance(transform.position, specialAttackPoints[0]) < 0.2f) {
                    specialAttackStage = 999;
                } else {
                    Run();
                    transform.position = Vector2.MoveTowards(transform.position, specialAttackPoints[0], extraDashSpeed * npcObject.speed * Time.deltaTime);
                    TryAttack(currentDamage);
                }
                break;
            default:
                specialAttackStage = 0;
                Physics2D.IgnoreCollision(transform.gameObject.GetComponent<Collider2D>(), target.gameObject.GetComponent<Collider2D>(), false);
                //Physics2D.IgnoreLayerCollision(12, 11, false);
                break;
        }
    }

    void StartBossMusic()
    {
        if (!checkBossSound)
        {
            SoundManager.instance.Play("BossFight1");
            checkBossSound = true;
        }
    }

    private void Jump() {
        animatorNpc.SetTrigger("Jump");
    }

    private void Transformation() {
        SoundManager.instance.Play("Orc");
        animatorNpc.SetTrigger("Transform");
        currentDamage += extraDmg;
    }
}
