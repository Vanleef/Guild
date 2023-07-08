using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public string characterClass;
    public int currentHp;
    public Rigidbody2D rigidBody;
    public bool isInvulnerable;
    public bool isDodging;
    public Transform attackPos;
    public LayerMask WhatIsEnemies;
    private ClassActions actions;
    public Animator animator;
    public CharacterObject character;
    public ParticleSystem hitParticles;
    private float nextAttack = float.NegativeInfinity;
    public bool isAlive = true;
    private float lastHit = -9999f;
    [SerializeField]
    private float hitInvulnerabilityDuration = 0.5f;

    private void Start()
    {
        // currentHp = character.HP;
        ClassActionsFactory actionsFactory = new ClassActionsFactory();
        actions = actionsFactory.getClassActions(gameObject, characterClass);
        InitActions();
    }

    private void Update(){
        if(lastHit + hitInvulnerabilityDuration < Time.time)
        {
            actions.isInvulnerable = false;
        }
    }

    private void InitActions()
    {
        actions.rigidBody = rigidBody;
        actions.attackPos = attackPos;
        actions.animator = animator;
        actions.character = character;
        actions.particles = hitParticles;
        actions.whatIsEnemies = WhatIsEnemies;
        actions.isInvulnerable = isInvulnerable;
        actions.isDodging = isDodging;
    }


    public void TakeDamage(int damage)
    {
        if((!actions.isDodging) && (!actions.isInvulnerable)){      //if is not dodging and not invulnerable => can be hit
            actions.GetHit();
            this.currentHp -= damage;
            actions.isInvulnerable = true;
            lastHit = Time.time;
            //executar animação de invulnerabilidade?
            if (this.currentHp <= 0)
            {
                currentHp = 0;
                isAlive = false;
                actions.Die();
            }
        }        
    }

    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            SoundManager.instance.Play(character.attackSfx);
            actions.Attack();
            nextAttack = Time.time + character.AttackDelay;
        }

    }

    public void SpecialAttack()
    {
        actions.SpecialAttack();
    }

    public void SpecialMovement(int direction, float speed)
    {
        actions.SpecialMovement(direction, speed);
        SoundManager.instance.Play(character.moveSfx);
    }

}
