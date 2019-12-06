using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Equipment : TradeableItem
{
    //Character
    [SerializeField] private warriorType[] classRestriction;
    [SerializeField] private int levelRequirement;


    //Public
    public warriorType[] ClassRestriction { get { return classRestriction; } set { classRestriction = value; } }
    public int LevelRequirement { get { return levelRequirement; } set { levelRequirement = value; } }
}
