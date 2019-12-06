using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType
{
    collect,
    slay,
    talk
}

[System.Serializable]
public class QuestObjective
{
    //Objective
    [SerializeField] ObjectiveType objective;
    [SerializeField] private bool objectiveComplete;
    [SerializeField] private float objectiveAmount;
    [SerializeField] private float objectiveProgress;
    [SerializeField] private string objectiveDescription;
    [SerializeField] private string objectiveTarget;


    //Public
    public ObjectiveType Objective { get { return objective; } }
    public bool ObjectiveComplete { get { return objectiveComplete; } }
    public float ObjectiveAmount { get { return objectiveAmount; } }
    public float ObjectiveProgress { get {return objectiveProgress; } }
    public string ObjectiveDescription { get { return objectiveDescription; } }
    public string ObjectiveTarget { get { return objectiveTarget; } }

    public void ProgressObjective()
    {
        objectiveProgress++;

        if (objectiveProgress >= objectiveAmount)
        {
            ObjectiveCompleted();
        }
    }

    public void ProgressObjective(int progressAmount)
    {
        objectiveProgress += progressAmount;

        if (objectiveProgress >= objectiveAmount)
        {
            ObjectiveCompleted();
        }
    }

    public void ObjectiveCompleted()
    {
        objectiveComplete = true;
    }

}
