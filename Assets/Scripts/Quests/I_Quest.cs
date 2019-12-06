using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class I_Quest
{
    //Quest
    [SerializeField] private string questID;
    [SerializeField] private string questName;
    [SerializeField] private string questDescription;
    [SerializeField] private string questGiverDialog;
    [SerializeField] private string questEndDialog;
    [SerializeField] private bool questCompleted;
    [SerializeField] private bool hasFollowUpObjective;
    [SerializeField] private List<QuestObjective> questObjectives = new List<QuestObjective>();
    [SerializeField] private QuestObjective followUpObjective;
    [SerializeField] private I_item[] itemReward;
    [SerializeField] private float goldReward;
    [SerializeField] private float expReward;


    //Public
    public string QuestID { get { return questID; } }
    public string QuestName { get { return questName; } }
    public string QuestDescription { get { return questDescription; } }
    public string QuestGiverDialog { get { return questGiverDialog; } }
    public string QuestEndDialog { get { return questEndDialog; } }
    public bool QuestCompleted { get { return questCompleted; } }
    public bool HasFollowUpObjective { get { return hasFollowUpObjective; } set { hasFollowUpObjective = value; } }
    public List<QuestObjective> QuestObjectives { get { return questObjectives; } }
    public QuestObjective FollowUpObjective { get { return followUpObjective; } }
    public I_item[] ItemReward { get { return itemReward; } }
    public float GoldReward { get { return goldReward; } }
    public float ExpReward { get { return expReward; } } 

    public void SetFollowUpObjective()
    {
        questObjectives.Clear();
        questObjectives.Add(followUpObjective);
    }

    public void QuestComplete()
    {
        questCompleted = true;
    }
}
