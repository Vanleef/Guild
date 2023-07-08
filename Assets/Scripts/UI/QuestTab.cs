using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestTab : MonoBehaviour
{
    bool isShowing = false;
    [SerializeField]
    private GameObject mainQuestList;
    [SerializeField]
    private GameObject sideQuestList;
    private List<Quest> activeQuests;
    [SerializeField]
    private GameObject mainQuestItem;
    [SerializeField]
    private GameObject sideQuestItem;
    [SerializeField]
    private GameObject questTab;
    private GameObject xpBar;
    private Slider xpSlider;
    private 
    void Start()
    {
        xpBar = questTab.transform.parent.Find("XPBar").gameObject;
        xpSlider = xpBar.GetComponentInChildren<Slider>();
        xpSlider.maxValue = 0;
        xpBar.SetActive(isShowing);
        questTab.SetActive(isShowing);
        setXpMaxValue();
    }

    void Awake()
    {
        
    }

    void setXpMaxValue()
    {
        foreach (Quest quest in QuestManager.Instance.quests)
        {
            xpSlider.maxValue += quest.reward;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ToggleVisible();
            OpenQuestTab();
            OpenXpBar();
        }
    }

    void OpenQuestTab()
    {
        if (isShowing) FillQuests();
        else ClearList();

        questTab.SetActive(isShowing);
    }

    public void OpenXpBar()
    {
        UpdateXp();
        xpBar.SetActive(isShowing);
    }

    void ToggleVisible()
    {
        if (isShowing) isShowing = false;
        else isShowing = true;
    }

    void FillQuests()
    {
        activeQuests = QuestManager.Instance.activeQuests;
        foreach (Quest quest in activeQuests)
        {
            if(quest != null)
            {
                if (quest.isMainQuest)
                {
                    Instantiate(mainQuestItem, mainQuestList.transform);
                    TMP_Text questText = mainQuestItem.transform.Find("QuestText").GetComponent<TMP_Text>();
                    questText.text = quest.goal.description +
                        " " + quest.goal.currentAmount +
                        " / " + quest.goal.requiredAmount;
                }
                else
                {
                    Instantiate(sideQuestItem, sideQuestList.transform);
                    TMP_Text questText = sideQuestItem.transform.Find("QuestText").GetComponent<TMP_Text>();
                    questText.text = quest.goal.description +
                        " " + quest.goal.currentAmount +
                        " / " + quest.goal.requiredAmount;
                }
            }
        }

    }

    void UpdateXp()
    {
        TMP_Text xpText = xpBar.transform.Find("Value").GetComponent<TMP_Text>();
        List<Quest> completedQuests = new List<Quest>();
        for (int i=0; i<QuestManager.Instance.quests.Count; i++)
        {
            Quest quest = QuestManager.Instance.quests[i];
            if (quest != null)
            {
                if (quest.goal.isComplete && QuestManager.Instance.gainXp)
                {
                    completedQuests.Add(quest);
                    xpSlider.value += quest.reward;
                    QuestManager.Instance.gainXp = false;
                }
            }
        }
        xpText.text = xpSlider.value + "/" + xpSlider.maxValue;
    }

    void ClearList()
    {
        foreach (Transform child in mainQuestList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in sideQuestList.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
