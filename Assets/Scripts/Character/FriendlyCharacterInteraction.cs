using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct dialogue
{
    public string[] dialogues;
}

public class FriendlyCharacterInteraction : MonoBehaviour
{
    private QuestManager qManager;
    private FriendlyCharacterStats characterStats;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject questDialogBox;

    [SerializeField] private bool questGiver;
    [SerializeField] private string[] FriendlyQuestIDList;
    [SerializeField] private dialogue[] dialogueTree;

    private int questIndex = 0;
    private bool questHanded = false;
    private bool questAvailable;
    private bool awaitingAnswer;

    void Start()
    {
        qManager = QuestManager.Instance;

        if (questGiver)
        {
            questAvailable = true;
        }
    }

    void Update()
    {
        if (awaitingAnswer)
        {
            if (questDialogBox.activeSelf)
            {
                if(Input.GetKeyDown(KeyCode.Keypad2))
                {
                    QuestManager.Instance.AddQuest(FriendlyQuestIDList[questIndex]);
                    awaitingAnswer = false;
                    questHanded = true;
                    CleanDialogBox();
                }

                if(Input.GetKeyDown(KeyCode.Keypad6))
                {
                    awaitingAnswer = false;
                    CleanDialogBox();
                }    
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Keypad2))
                {
                    awaitingAnswer = false;
                    CleanDialogBox();
                }
            }
        }
    }

    public void Interact()  
    {
        string possibleQuestID = QuestManager.Instance.FindRelevancy(transform.name);
        qManager.CharacterInteraction(gameObject.name);

        if (questGiver)
        {
            if (questHanded)
            {
                if (QuestManager.Instance.GetQuest(FriendlyQuestIDList[questIndex]).QuestCompleted)
                {
                    GenerateDialog(QuestManager.Instance.GetQuest(FriendlyQuestIDList[questIndex]).QuestEndDialog);
                    questHanded = false;

                    if (questIndex + 1 < FriendlyQuestIDList.Length)
                    {
                        questIndex++;
                        questAvailable = true;
                    }
                    else
                    {
                        questAvailable = false;
                    }
                }
                else
                {
                    GenerateDialog(dialogueTree[0].dialogues[Random.Range(0, dialogueTree[0].dialogues.Length)]);
                }
            }
            else
            {
                if (questAvailable)
                {
                    I_Quest quest = qManager.GetQuest(FriendlyQuestIDList[questIndex]);
                    GenerateQuestOffer(quest.QuestGiverDialog, quest.GoldReward, quest.ExpReward);
                }
                else
                {
                    GenerateDialog(dialogueTree[1].dialogues[Random.Range(0, dialogueTree[1].dialogues.Length)]);
                }
            }
        }
        else if (possibleQuestID != null)
        {
            Debug.Log("Sup");
            GenerateDialog(QuestManager.Instance.GetQuest(possibleQuestID).QuestEndDialog);
        }
        else
        {
            GenerateDialog(dialogueTree[0].dialogues[Random.Range(0, dialogueTree[0].dialogues.Length)]);
        }
    }

    private void GenerateDialog(string dialog)
    {
        dialogBox.SetActive(true);
        dialogBox.transform.GetChild(1).GetComponent<Text>().text = dialog;
        awaitingAnswer = true;
    }

    private void GenerateQuestOffer(string dialog, float goldReward, float expReward)
    {
        questDialogBox.SetActive(true);
        questDialogBox.transform.GetChild(1).GetComponent<Text>().text = dialog;
        questDialogBox.transform.GetChild(3).GetComponent<Text>().text = "Reward: +" + goldReward + " Gold / +" + expReward + " Exp";
        awaitingAnswer = true;
    }

    private void CleanDialogBox()
    {
        if (dialogBox.activeSelf)
        {
            dialogBox.SetActive(false);
        }

        if (questDialogBox.activeSelf)
        {
            questDialogBox.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Player>().FriendlyOnRange(gameObject.GetComponent<FriendlyCharacterInteraction>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Player>().FriendlyOutOfRange();
        }
    }
}
