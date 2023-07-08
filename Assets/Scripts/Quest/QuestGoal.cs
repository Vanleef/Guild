using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal
{
    public string description;
    public bool isComplete;
    public int currentAmount;
    public int requiredAmount;
    public string targetID;

    public QuestGoal(string description, int requiredAmount, string targetID) {
        this.description = description;
        this.isComplete = false;
        this.currentAmount = 0;
        this.requiredAmount = requiredAmount;
        this.targetID = targetID;
    }

    public void GoalProgress()
    {
        if (!this.isComplete)
        {
            this.currentAmount++;
            if (this.currentAmount == this.requiredAmount)
            {
                this.isComplete = true;
            }
        }
    }
}
