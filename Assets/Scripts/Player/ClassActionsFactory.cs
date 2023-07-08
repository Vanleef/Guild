using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassActionsFactory
{
    public ClassActions getClassActions(GameObject obj, string charClass)
    {
        ClassActions action;
        switch (charClass.ToLower())
        {
            case "warrior": action = obj.AddComponent<WarriorActions>(); return action;
            case "archer": action = obj.AddComponent<ArcherActions>(); return action;
            case "mage": action = obj.AddComponent<MageActions>(); return action;
            default: return new WarriorActions();
        }
    }
}
