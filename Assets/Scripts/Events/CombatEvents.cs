using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public delegate void EnemyEventHandler(string enemyID);
    public static event EnemyEventHandler OnEnemyDeath;

    public static void EnemyDied(string enemyID)
    {
        if (OnEnemyDeath != null)
            OnEnemyDeath(enemyID);
    }
}
