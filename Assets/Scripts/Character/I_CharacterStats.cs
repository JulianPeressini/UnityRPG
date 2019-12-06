using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class I_CharacterStats
{
    //Character
    private float health;
    private float mana;
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;


    //Public
    public float Health { get { return health; } set { health = value; } }
    public float Mana { get { return mana; } set { mana = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public float MaxMana { get { return maxMana; } set { maxMana = value; } }
}
