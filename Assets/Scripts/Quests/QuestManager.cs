using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class QuestManager : MonoBehaviour
{

    static private QuestManager instance;
    static public QuestManager Instance { get { return instance; } }

    [SerializeField] private GameObject questLog;
    [SerializeField] private GameObject questEntry;

    QuestLogUI qlUI;

    [SerializeField] private List<I_Quest> questList;
    [SerializeField] private List<I_Quest> activeQuestList;
    [SerializeField] private List<string> activeQuestListKeys;

    [SerializeField] private Player player;
    private PlayableCharacterStats playerStats;

    public List<I_Quest> QuestList { get { return questList; } }



    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            activeQuestListKeys = new List<string>();
            playerStats = player.GetComponent<Player>().PlayerStats;
            qlUI = new QuestLogUI(questLog, questEntry);
            DontDestroyOnLoad(gameObject);
        }
    }

    public I_Quest GetQuest(string questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].QuestID == questID)
            {
                return questList[i];
            }
        }

        return null;
    }

    public void AddQuest(I_Quest newQuest)
    {
        activeQuestList.Add(newQuest);
        activeQuestListKeys.Add(newQuest.QuestID);
    }

    public void AddQuest(string newQuestID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].QuestID == newQuestID)
            {
                activeQuestList.Add(questList[i]);
                activeQuestListKeys.Add(newQuestID);
                qlUI.AddQuest(questList[i]);
            }
        }
    }

    public void RemoveQuest(I_Quest quest)
    {
        activeQuestList.Remove(quest);
        activeQuestListKeys.Remove(quest.QuestID);
        qlUI.RemoveQuest(quest.QuestID);
    }

    public void ItemAquired(I_item item)
    {
        for (int i = 0; i < activeQuestList.Count; i++)
        {
            for (int j = 0; j < activeQuestList[i].QuestObjectives.Count; j++)
            {
                if (CheckObjective(activeQuestList[i].QuestObjectives[j], ObjectiveType.collect))
                {
                    if (activeQuestList[i].QuestObjectives[j].ObjectiveTarget == item.Name)
                    {
                        if (!activeQuestList[i].QuestObjectives[j].ObjectiveComplete)
                        {
                            activeQuestList[i].QuestObjectives[j].ProgressObjective();
                            if (CheckQuestStatus(activeQuestList[i]))
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    public void CharacterInteraction(string characterName)
    {
        for (int i = 0; i < activeQuestList.Count; i++)
        {
            for (int j = 0; j < activeQuestList[i].QuestObjectives.Count; j++)
            {
                if (CheckObjective(activeQuestList[i].QuestObjectives[j], ObjectiveType.talk))
                {
                    if (activeQuestList[i].QuestObjectives[j].ObjectiveTarget == characterName)
                    {
                        if (!activeQuestList[i].QuestObjectives[j].ObjectiveComplete)
                        {
                            activeQuestList[i].QuestObjectives[j].ProgressObjective();
                            if (CheckQuestStatus(activeQuestList[i]))
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    public void MonsterSlayed(string characterName)
    {
        for (int i = 0; i < activeQuestList.Count; i++)
        {
            for (int j = 0; j < activeQuestList[i].QuestObjectives.Count; j++)
            {
                if (CheckObjective(activeQuestList[i].QuestObjectives[j], ObjectiveType.slay))
                {
                    if (activeQuestList[i].QuestObjectives[j].ObjectiveTarget == characterName)
                    {
                        if (!activeQuestList[i].QuestObjectives[j].ObjectiveComplete)
                        {
                            activeQuestList[i].QuestObjectives[j].ProgressObjective();
                            if (CheckQuestStatus(activeQuestList[i]))
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    public string FindRelevancy(string relevantTarget)
    {
        for (int i = 0; i < activeQuestList.Count; i++)
        {
            for (int j = 0; j < activeQuestList[i].QuestObjectives.Count; j++)
            {
                if (activeQuestList[i].QuestObjectives[j].ObjectiveTarget == relevantTarget)
                {
                    return activeQuestList[i].QuestID;
                }
            }
        }

        return null;
    }

    private bool CheckObjective(QuestObjective questObjective, ObjectiveType targetObjective)
    {
        if (questObjective.Objective == targetObjective)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckQuestStatus(I_Quest quest)
    {
        for (int i = 0; i < quest.QuestObjectives.Count; i++)
        {
            if (!quest.QuestObjectives[i].ObjectiveComplete)
            {
                return false;
            }
        }

        if (quest.HasFollowUpObjective)
        {
            quest.SetFollowUpObjective();
            quest.HasFollowUpObjective = false;
        }
        else
        {
            QuestCompleted(quest);
        }

        return true;
    }

    private void QuestCompleted(I_Quest quest)
    {
        quest.QuestComplete();
        RemoveQuest(quest);
        playerStats.Gold += quest.GoldReward;
        playerStats.Experience += quest.ExpReward;
        Inventory.Instance.UpdateStatInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class QuestLogUI
{
    private GameObject refQuestLog;
    private GameObject entryPrefab;

    private List<I_Quest> quests;
    private List<GameObject> entrys;

    public QuestLogUI(GameObject _questPanel, GameObject _entryPrefab)
    {
        refQuestLog = _questPanel;
        entryPrefab = _entryPrefab;
        quests = new List<I_Quest>();
        entrys = new List<GameObject>();
    }

    public void AddQuest(I_Quest questToAdd)
    {
        GameObject newQuestEntry = GameObject.Instantiate(entryPrefab);
        newQuestEntry.transform.GetChild(0).GetComponent<Text>().text = questToAdd.QuestName;
        newQuestEntry.transform.GetChild(1).GetComponent<Text>().text = questToAdd.QuestDescription;
        newQuestEntry.transform.SetParent(refQuestLog.transform);

        for (int i = 0; i < questToAdd.QuestObjectives.Count; i++)
        {
            newQuestEntry.transform.GetChild(2).transform.GetChild(i).GetComponent<Text>().text = questToAdd.QuestObjectives[i].ObjectiveDescription;
            newQuestEntry.transform.GetChild(3).transform.GetChild(i).GetComponent<Text>().text = questToAdd.QuestObjectives[i].ObjectiveProgress + "/" + questToAdd.QuestObjectives[i].ObjectiveAmount;
        }

        quests.Add(questToAdd);
        entrys.Add(newQuestEntry);
    }

    public void RemoveQuest(string questID)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (questID == quests[i].QuestID)
            {
                quests.RemoveAt(i);
                GameObject.Destroy(entrys[i]);
                entrys.RemoveAt(i);
            }
        }
    }

    public void ProgressObjective(string questID)
    {

    }
}