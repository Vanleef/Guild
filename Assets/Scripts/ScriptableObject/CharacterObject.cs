using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CharacterObject : ScriptableObject
{
    public int HP;
    public int MaxHP;
    public float Speed;
    public int AttackDamage;
    public float AttackDelay;
    public int Defense;
    public float SpecialMoveCD;
    public float attackRange;
    public string attackSfx;
    public string jumpSfx;
    public string moveSfx;

}
