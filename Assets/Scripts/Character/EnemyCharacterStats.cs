using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyCharacterStats : I_CharacterStats
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float armor;

    public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
    public float Armor { get { return armor; } set { armor = value; } }
}