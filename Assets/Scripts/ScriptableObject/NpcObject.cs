using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class NpcObject : ScriptableObject
{
    public string npcId;
    public int hp;
    public float speed;
    public int attackDamage;
    public float detectionRangeX;
    public float detectionRangeY;
    public float attackRangeX;
    public float attackRangeY;
    public float attackSpeed;
}
