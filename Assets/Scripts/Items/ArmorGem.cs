using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmorGem : Equipment
{
    //Physical resistance
    [SerializeField] private float armBonus;
    [SerializeField] private float hpRegenBonus;
    [SerializeField] private float mpRegenBonus;


    //Stats
    [SerializeField] private float strBonus;
    [SerializeField] private float dexBonus;
    [SerializeField] private float intBonus;


    //Element resistance
    [SerializeField] private float fireResBonus;
    [SerializeField] private float waterResBonus;
    [SerializeField] private float earthResBonus;
    [SerializeField] private float windResBonus;


    //Tradeable Item
    public override Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> itemStats = new Dictionary<string, float>();

        itemStats.Add("Armor", armBonus);
        itemStats.Add("Health regeneration", hpRegenBonus);
        itemStats.Add("Mana regeneration", mpRegenBonus);
        itemStats.Add("Strength", strBonus);
        itemStats.Add("Dexterity", dexBonus);
        itemStats.Add("Intelligence", intBonus);
        itemStats.Add("Fire resistance", fireResBonus);
        itemStats.Add("Water resistance", waterResBonus);
        itemStats.Add("Earth resistance", earthResBonus);
        itemStats.Add("Wind resistance", windResBonus);

        return itemStats;
    }

    //Public
    public float ArmBonus { get { return armBonus; } set { armBonus = value; } }
    public float HpRegenBonus { get { return hpRegenBonus; } set { hpRegenBonus = value; } }
    public float MpRegenBonus { get { return mpRegenBonus; } set { mpRegenBonus = value; } }

    public float StrBonus { get { return strBonus; } set { strBonus = value; } }
    public float DexBonus { get { return dexBonus; } set { dexBonus = value; } }
    public float IntBonus { get { return intBonus; } set { intBonus = value; } }

    public float FireResBonus { get { return fireResBonus; } set { fireResBonus = value; } }
    public float WaterResBonus { get { return waterResBonus; } set { waterResBonus = value; } }
    public float EarthResBonus { get { return earthResBonus; } set { earthResBonus = value; } }
    public float WindResBonus { get { return windResBonus; } set { windResBonus = value; } }
}
