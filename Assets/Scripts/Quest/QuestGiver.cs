using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;

public class QuestGiver : MonoBehaviour
{
    public static QuestGiver instance { get; set; }
    public List<Quest> quests { get; set; }

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Quest Giver Started");
            instance = this;
        }
        LoadQuests();
        AcceptAllQuests();
    }

    void Update()
    {
    }

    private void LoadQuests()
    {
        //quests = JsonConvert.DeserializeObject<List<Quest>>(Resources.Load<TextAsset>("JSON/quests").ToString());
        Debug.Log(quests.ToString());
    }

    private void AcceptAllQuests (){
        foreach (Quest quest in quests)
        {
            QuestManager.Instance.AddQuest(quest);
        }
    }

    public void AcceptQuest(int index)
    {
        QuestManager.Instance.AddQuest(quests[index]);
    }
}
