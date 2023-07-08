using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public bool startFacingRight;
    protected bool facingRight;
    protected Transform target;
    public NpcObject npcObject;
    public Animator animatorNpc;
    protected int currentHp;
    //protected float waitDeathTime = 3f;
    protected float waitDeathTime = 0.3f;
    protected float waitHitTime;
    protected float waitAttackTime;
    [SerializeField]
    protected float patrolDistance;
    private Vector2 patrolSpot;
    private float speed;
    [SerializeField]
    protected float extraSpeed = 1f;
    private Vector2 originalPosition;
    protected bool chasingTarget = false;
    protected float knockback;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private GameObject dropPotion;

    void Start() {
        StartNpc();
        StartPatrol();
    }

    // Call it on Start()
    protected void StartNpc() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        facingRight = true;
        if (startFacingRight == false) {
            Flip();
        }
        currentHp = npcObject.hp;
        originalPosition = transform.position;
        speed = npcObject.speed;
    }

    void Update(){
        if (currentHp > 0) {
            if ((chasingTarget == false) && (patrolDistance > 0)) {
                Patroling();
            }
            UpdateChase();
            TryAttack(npcObject.attackDamage);
        } else {
            if (waitDeathTime <= 0) {
                Destroy(gameObject);
                return;
            } else {
                waitDeathTime -= Time.deltaTime;
            }
        }
    }

    // Call it on Start()
    protected void StartPatrol() {
        if (startFacingRight == true) {
            patrolSpot = new Vector2(transform.position.x + patrolDistance, transform.position.y);
        } else {
            patrolSpot = new Vector2(transform.position.x - patrolDistance, transform.position.y);
        }
    }

    // Call it on Update()
    protected void Patroling() {
        Walk();
        if (waitHitTime <= 0) {
            transform.position = Vector2.MoveTowards(transform.position, patrolSpot, speed * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, target.position, (speed - knockback) * Time.deltaTime);
            waitHitTime -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, patrolSpot) < 0.2f) {
            Flip();
            if (facingRight == true) {
                patrolSpot.x += patrolDistance;
            } else {
                patrolSpot.x -= patrolDistance;
            }
        }
    }

    // Call it on Update()
    protected void UpdateChase() {
        if (chasingTarget == true) {
            if (Vector2.Distance(transform.position, target.position) <= npcObject.detectionRangeX + 5f) {
                Run();
                Chasing();
            }  else {
                BackToPosition();
            }
        } else {
            SearchPlayer();
        }
    }

    // Search player to start the chase behavior
    protected void SearchPlayer() {
        if ((target.position.x > transform.position.x - npcObject.detectionRangeX) &&
            (target.position.x < transform.position.x + npcObject.detectionRangeX) && 
            (target.position.y > transform.position.y - npcObject.detectionRangeY) &&
            (target.position.y < transform.position.y + npcObject.detectionRangeY)) {
            startFacingRight = facingRight;
            chasingTarget = true;
        }
    }

    // NPC runs to the player
    protected void Chasing() {
        if (waitHitTime <= 0) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, extraSpeed * speed * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, target.position, (speed - knockback) * Time.deltaTime);
            waitHitTime -= Time.deltaTime;
        }
        LookToPlayer();
    }

    // NPC faces the player direction
    protected void LookToPlayer() {        
        if ((facingRight == true && transform.position.x > target.position.x) ||
        (facingRight == false && transform.position.x < target.position.x)) {
            Flip();
        }
    }

    // NPC tries to get back to its position
    // If the player gets too far the NPC teleports back to its position
    protected void BackToPosition() {
        if (Vector2.Distance(transform.position, target.position) <= 50f) {
            Walk();
            if ((facingRight == true && transform.position.x > originalPosition.x) ||
            (facingRight == false && transform.position.x < originalPosition.x)) {
                Flip();
            }
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            // Checks if the NPC is already on original position
            if (Vector2.Distance(transform.position, originalPosition) < 0.2f) {
                Guard();
                RestoreHp();
                chasingTarget = false;
                if (facingRight != startFacingRight) {
                    Flip();
                }
            }
        } else {
            transform.position = originalPosition;
            Guard();
            RestoreHp();
        }
    }

    // NPC checks if the player is on range to attack
    protected void TryAttack(int dmg) {
        if (waitAttackTime <= 0) {
            if ((target.position.x > transform.position.x - npcObject.attackRangeX) &&
            (target.position.x < transform.position.x + npcObject.attackRangeX) && 
            (target.position.y > transform.position.y - npcObject.attackRangeY) &&
            (target.position.y < transform.position.y + npcObject.attackRangeY)) {
                Attack();
                GameController.Instance.DamagePlayer(target.gameObject, dmg);
                waitAttackTime = npcObject.attackSpeed;
            }
        } else {
            waitAttackTime -= Time.deltaTime;
        }
    }

    protected bool ThereIsGround() {
        bool isGround = true;

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f);
        if (groundInfo.collider == false) {
            isGround = false;
        }

        return isGround;
    }

    protected void RestoreHp() {
        currentHp = npcObject.hp;
    }

    // Animation functions
    protected void Run() {
        animatorNpc.SetBool("isWalking", false);
        animatorNpc.SetBool("isRunning", true);
    }

    protected void Walk() {
        animatorNpc.SetBool("isWalking", true);
        animatorNpc.SetBool("isRunning", false);
    }

    protected void Guard() {
        animatorNpc.SetBool("isWalking", false);
        animatorNpc.SetBool("isRunning", false);
    }

    protected void Attack() {
        animatorNpc.SetTrigger("Attack");
    }

    // Decreases NPC's hp, sets knockback if exists
    public void Hit(int damage, float kb = 0f) {
        animatorNpc.SetTrigger("Hit");
        currentHp -= damage;
        if (currentHp <= 0) {
            Die();
        } else {
            knockback = kb;
            waitHitTime = 0.5f;
        }
    }

    // Turn off collision with player
    protected void Die() {
        CombatEvents.EnemyDied(npcObject.npcId);
        PlayVictorySound(npcObject.npcId);
        animatorNpc.SetTrigger("Death");
        //Destroy(transform.gameObject.GetComponent<Collider2D>());
        //Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
        Physics2D.IgnoreCollision(transform.gameObject.GetComponent<Collider2D>(), target.gameObject.GetComponent<Collider2D>());
        if (dropPotion != false) {
            float spawnPotion;
            spawnPotion = Random.Range(1f,100f);
            if (spawnPotion <= 25f) {
                GameObject hpPotion = Instantiate(dropPotion, transform.position, transform.rotation);
            }
        }
    }


    IEnumerator VictorySound()
    {
        SoundManager.instance.Play("Win");
        yield return new WaitForSeconds(6);
        SoundManager.instance.Play("Level 1");
    }

    void PlayVictorySound(string id)
    {
        if (id == "003.Moldrak" || id == "008.Skarlett") VictorySound();
    }

    // NPC turns to the other side
    protected void Flip() {
        if (facingRight == true) {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;
        } else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            speed = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            speed = npcObject.speed;
        }
    }

}
