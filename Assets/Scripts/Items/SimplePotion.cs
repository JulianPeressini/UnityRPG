using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimplePotion : Consumable
{
    //Healing
    [SerializeField] private float hpGain;
    [SerializeField] private float mpGain;
    [SerializeField] private float recoveryTime;


    //Tradeable Item
    public override Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> itemStats = new Dictionary<string, float>();

        itemStats.Add("Health", hpGain);
        itemStats.Add("Mana", mpGain);
        itemStats.Add("Recovery time", recoveryTime);

        return itemStats;
    }


    //Potion
    public override void Consume(Player player)
    {

    }


    //Public
    public float HpGain { get { return hpGain; } set { hpGain = value; } }
    public float MpGain { get { return mpGain; } set { mpGain = value; } }
    public float RecoveryTime { get { return recoveryTime; } set { recoveryTime = value; } }
}
