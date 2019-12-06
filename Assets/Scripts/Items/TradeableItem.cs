using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TradeableItem : I_item
{
    //Gold
    [SerializeField] private float goldValue;
    [SerializeField] private float goldValueOffset;


    //Tradeable item
    [SerializeField] public abstract Dictionary<string, float> GetStats();


    //Public
    public float GoldValue { get { return goldValue; } set { goldValue = value; } }
    public float GoldValueOffset { get { return goldValueOffset; } set { goldValueOffset = value; } }
}
