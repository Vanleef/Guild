using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; set; }
    public List<Quest> activeQuests { get; set; }
    public List<Quest> quests { get; set; }
    public GameObject questCompleted;
    public bool gainXp = false;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Quest Manager Started");
            Instance = this;
        }
        CombatEvents.OnEnemyDeath += EnemyDied;
        activeQuests = new List<Quest>();
        LoadQuests();
        AcceptAllQuests();
        Debug.Log("Quests:" + activeQuests.Count);
    }

    void EnemyDied(string enemyID)
    {
        Debug.Log("enemy died " + enemyID);
        foreach (Quest quest in activeQuests)
        {
            if (quest.goal.targetID == enemyID)
            {
                Debug.Log("Progrediu a quest");
                quest.goal.GoalProgress();
                EvaluateQuests();
            }
        }
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        Debug.Log(quest.goal.targetID);
    }

    IEnumerator waiter(Quest quest)
    {
        yield return new WaitForSeconds(3);
        questCompleted.SetActive(false);
        activeQuests.Remove(quest);

    }

    void EvaluateQuests()
    {
        Debug.Log("Entrei no evaluate");
        foreach (Quest quest in activeQuests)
        {
            if (quest.Evaluate())
            {
                gainXp = true;
                SoundManager.instance.Play("Success");
                questCompleted.SetActive(true);
                TMP_Text completedText = questCompleted.transform.GetComponentInChildren<TMP_Text>();
                StartCoroutine(waiter(quest));

            }
            else
            {

            }
        }
    }

    private void LoadQuests()
    {
        quests = JsonConvert.DeserializeObject<List<Quest>>(Resources.Load<TextAsset>("JSON/quests").ToString());
        Debug.Log(quests.ToString());
    }

     private void AcceptAllQuests (){
        foreach (Quest quest in quests)
        {
            activeQuests.Add(quest);
        }
    }

}
