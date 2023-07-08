using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassActions :MonoBehaviour
{
    public Animator animator { get; set; }
    public CharacterObject character { get; set; }
    public ParticleSystem particles { get; set; }
    public LayerMask whatIsEnemies { get; set; }
    public Transform attackPos { get; set; }

    public Rigidbody2D rigidBody { get; set; }
    public bool isInvulnerable { get; set; }
    public bool isDodging { get; set; }

    public abstract void Attack();
    public abstract void SpecialMovement(int direction = 1, float speed = 0.1f);
    public abstract void SpecialAttack();
    public abstract void GetHit();
    public abstract void Die();
}
