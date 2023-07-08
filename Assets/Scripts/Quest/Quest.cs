using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string ID;
    public bool isMainQuest;
    public int requiredLevel;
    public int reward;
    public QuestGoal goal;

    public Quest(string ID, bool isMainQuest, int requiredLevel, int reward, QuestGoal goal)
    {
       this.ID = ID;
       this.isMainQuest = isMainQuest;
       this.requiredLevel = 1;
       this.reward = 100;
       this.goal = goal;
    }

    public bool Evaluate() {
        return this.goal.isComplete;
    }

}
