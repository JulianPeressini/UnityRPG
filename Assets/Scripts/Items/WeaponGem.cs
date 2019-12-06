using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponGem : Equipment
{
    //Attack damage
    [SerializeField] private float chargePwrBonus;
    [SerializeField] private float atkDmgBonus;


    //Stats
    [SerializeField] private float strBonus;
    [SerializeField] private float dexBonus;
    [SerializeField] private float intBonus;


    //Element damage
    [SerializeField] private float fireDmgBonus;
    [SerializeField] private float waterDmgBonus;
    [SerializeField] private float earthDmgBonus;
    [SerializeField] private float windDmgBonus;


    //Tradeable Item
    public override Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> itemStats = new Dictionary<string, float>();

        itemStats.Add("Charge power", chargePwrBonus);
        itemStats.Add("Attack damage", atkDmgBonus);
        itemStats.Add("Strength", strBonus);
        itemStats.Add("Dexterity", dexBonus);
        itemStats.Add("Intelligence", intBonus);
        itemStats.Add("Fire damage", fireDmgBonus);
        itemStats.Add("Water damage", waterDmgBonus);
        itemStats.Add("Earth damage", earthDmgBonus);
        itemStats.Add("Wind damage", windDmgBonus);

        return itemStats;
    }


    //Public
    public float ChargePwrBonus { get { return chargePwrBonus; } set { chargePwrBonus = value; } }
    public float AtkDmgBonus { get { return atkDmgBonus; } set { atkDmgBonus = value; } }

    public float StrBonus { get { return strBonus; } set { strBonus = value; } }
    public float DexBonus { get { return dexBonus; } set { dexBonus = value; } }
    public float IntBonus { get { return intBonus; } set { intBonus = value; } }

    public float FireDmgBonus { get { return fireDmgBonus; } set { fireDmgBonus = value; } }
    public float WaterDmgBonus { get { return waterDmgBonus; } set { waterDmgBonus = value; } }
    public float EarthDmgBonus { get { return earthDmgBonus; } set { earthDmgBonus = value; } }
    public float WindDmgBonus { get { return windDmgBonus; } set { windDmgBonus = value; } }
}
