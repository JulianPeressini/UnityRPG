using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stat
{
    Strength,
    Dexterity,
    Intelligence
}

public enum warriorType
{
    Paladin,
    Rogue,
    Mage,
    
}

[System.Serializable]
public class PlayableCharacterStats : I_CharacterStats
{
    //Character
    [SerializeField] private string name;
    [SerializeField] private warriorType characterClass;
    [SerializeField] private float characterLevel;
    [SerializeField] private float experience;
    [SerializeField] private float levelUpExp;
    [SerializeField] private float gold;


    //Stats
    [SerializeField] private stat primaryStat;

    [SerializeField] private float strength;
    private float strBonus;
    private float healthRegen;
    private float healthRegBonus;

    [SerializeField] private float dexterity;
    private float dexBonus;
    private float chargePower;
    private float chargePwrBonus;

    [SerializeField] private float intelligence;
    private float intBonus;
    private float manaRegen;
    private float manaRegBonus;


    //Damage
    [SerializeField] private float attackDamage;
    private float atkBonus;

    private float fireDamage;
    private float waterDamage;
    private float earthDamage;
    private float windDamage;


    //Resistances
    [SerializeField] private float armor;
    private float armBonus;

    private float fireResistance;
    private float waterResistance;
    private float earthResistance;
    private float windResistance;

    private float maxHealthBonus;
    private float maxManaBonus;


    //Public
    public string Name { get { return name; } set { name = value; } }
    public warriorType CharacterClass { get { return characterClass; } set { characterClass = value; } }
    public float CharacterLevel { get { return characterLevel; } set { characterLevel = value; } }
    public float Experience { get { return experience; } set { experience = value; } }
    public float LevelUpExp { get { return levelUpExp; } set { levelUpExp = value; } }
    public float Gold { get { return gold; } set { gold = value; } }


    public stat PrimaryStat { get { return primaryStat; } set { primaryStat = value; } }
    public float Strength { get { return strength; } set { strength = value; } }
    public float StrBonus { get { return strBonus; } set { strBonus = value; } }
    public float HealthRegen { get { return healthRegen; } set { healthRegen = value; } }
    public float HealthRegBonus { get { return healthRegBonus; } set { healthRegBonus = value; } }

    public float Dexterity { get { return dexterity; } set { dexterity = value; } }
    public float DexBonus { get { return dexBonus; } set { dexBonus = value; } }
    public float ChargePower {get { return chargePower; } set { chargePower = value;} }
    public float ChargePwrBonus { get { return chargePwrBonus; } set { chargePwrBonus = value; } }

    public float Intelligence { get { return intelligence; } set { intelligence = value; } }
    public float IntBonus { get { return intBonus; } set { intBonus = value; } }
    public float ManaRegen { get { return manaRegen; } set { manaRegen = value; } }
    public float ManaRegBonus { get { return manaRegBonus; } set { manaRegBonus = value; } }


    public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
    public float AtkBonus { get { return atkBonus; } set { atkBonus = value; } }


    public float FireDamage { get { return fireDamage; } set { fireDamage = value; } }
    public float WaterDamage { get { return waterDamage; } set { waterDamage = value; } }
    public float EarthDamage { get { return earthDamage; } set { earthDamage = value; } }
    public float WindDamage { get { return windDamage; } set { windDamage = value; } }


    public float Armor { get { return armor; } set { armor = value; } }
    public float ArmBonus { get { return armBonus; } set { armBonus = value; } }

    public float FireResistance { get { return fireResistance; } set { fireResistance = value; } }
    public float WaterResistance { get { return waterResistance; } set { waterResistance = value; } }
    public float EarthResistance { get { return earthResistance; } set { earthResistance = value; } }
    public float WindResistance { get { return windResistance; } set { windResistance = value; } }

    public float MaxHealthBonus { get { return maxHealthBonus; } set { maxHealthBonus = value; } }
    public float MaxManaBonus { get { return maxManaBonus; } set { maxManaBonus = value; } }


    //PlayableCharacterStats
    public void InitializeStats()
    {
        Health = MaxHealthBonus + ((Strength + StrBonus) * 10);
        Mana = MaxManaBonus + ((Intelligence + IntBonus) * 15);
        UpdateBaseStats();
    }

    public void UpdateBaseStats()
    {
        MaxHealth = MaxHealthBonus + ((Strength + StrBonus) * 10);
        HealthRegen = HealthRegBonus + ((Strength + StrBonus) * 0.1f);
        ChargePower = ChargePwrBonus + ((Dexterity + DexBonus) * 0.1f);
        Armor = ArmBonus + ((Dexterity + DexBonus) * 0.1f);
        MaxMana = MaxManaBonus + ((Intelligence + IntBonus) * 15);
        ManaRegen = ManaRegBonus + ((Intelligence + IntBonus) * 0.1f);

        switch (PrimaryStat)
        {
            case stat.Strength:

                AttackDamage = AtkBonus + (Strength + StrBonus);

                break;

            case stat.Dexterity:

                AttackDamage = AtkBonus + (Dexterity + DexBonus);

                break;

            case stat.Intelligence:

                AttackDamage = AtkBonus + (Intelligence + IntBonus);

                break;
        }
    }
}
