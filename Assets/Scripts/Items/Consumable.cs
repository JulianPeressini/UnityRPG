using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Consumable : TradeableItem
{
    //Potion
    [SerializeField] public abstract void Consume(Player player);
}
