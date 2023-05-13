using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour, IClassesParent, ITakeDamage
{
    private static WarriorController instance = null;

    public static WarriorController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WarriorController();
            }
            return instance;
        }
    }

    public void TakeDamage(int Damage)
    {
        //throw new System.NotImplementedException();
    }
    public void SpentEnergy(int Energy)
    {
        //throw new System.NotImplementedException();
    }

}
