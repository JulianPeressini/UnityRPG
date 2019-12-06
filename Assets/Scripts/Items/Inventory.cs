using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    static private Inventory instance;
    static public Inventory Instance { get { return instance; } }

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject itemInv;
    [SerializeField] private GameObject weaponGemInv;
    [SerializeField] private GameObject armorGemInv;
    [SerializeField] private GameObject invSlot;
    [SerializeField] private GameObject slotMarker;
    [SerializeField] private GameObject selectedSlotMarker;
    [SerializeField] private GameObject itemInfo;
    [SerializeField] private GameObject statsInfo;

    PlayableCharacterStats playerStats;

    private Slot[,] backpack;
    private Slot[,] weaponGemEquipment;
    private Slot[,] armorGemEquipment;
    private Slot[,] artifactEquipment;

    private InventoryUI bpUI;
    private InventoryUI wgUI;
    private InventoryUI agUI;

    [SerializeField] private int invSlotRows;
    [SerializeField] private int invSlotCols;
    [SerializeField] private int weaponGemRows;
    [SerializeField] private int weaponGemCols;
    [SerializeField] private int armorGemRows;
    [SerializeField] private int armorGemCols;
    [SerializeField] private int artifactRows;
    [SerializeField] private int artifactCols;

    private Marker invMarker;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeInventories();
            playerStats = player.GetComponent<Player>().PlayerStats;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeBasicStatsInfo();
    }

    private void InitializeInventories()
    {
        backpack = new Slot[invSlotRows, invSlotCols];
        weaponGemEquipment = new Slot[weaponGemRows, weaponGemCols];
        armorGemEquipment = new Slot[armorGemRows, armorGemCols];
        artifactEquipment = new Slot[artifactRows, artifactCols];

        bpUI = new InventoryUI(gameObject, itemInv, true);
        bpUI.GenerateInvUI(invSlotCols, invSlotRows, invSlot, slotMarker, selectedSlotMarker);
        wgUI = new InventoryUI(gameObject, weaponGemInv, false);
        wgUI.GenerateInvUI(weaponGemRows, weaponGemCols, invSlot, slotMarker, selectedSlotMarker);
        agUI = new InventoryUI(gameObject, armorGemInv, false);
        agUI.GenerateInvUI(armorGemRows, armorGemCols, invSlot, slotMarker, selectedSlotMarker);

        InitializeSlots(backpack, bpUI);
        InitializeSlots(weaponGemEquipment, wgUI);
        InitializeSlots(armorGemEquipment, agUI);
        InitializeSlots(artifactEquipment, agUI);

        invMarker = new Marker(backpack);
    }

    private void InitializeSlots(Slot[,] invToInit, InventoryUI slotInvUI)
    {
        for (int i = 0; i < invToInit.GetLength(0); i++)
        {
            for (int j = 0; j < invToInit.GetLength(1); j++)
            {
                invToInit[i, j] = new Slot(i, j);
                invToInit[i, j].CurrentInvUI = slotInvUI;
            }
        }
    }

    private void InitializeBasicStatsInfo()
    {
        statsInfo.transform.GetChild(0).GetComponent<Text>().text = playerStats.Name;
        statsInfo.transform.GetChild(1).GetComponent<Text>().text = "Level " + playerStats.CharacterLevel + " " + playerStats.CharacterClass.ToString() +  " (" + playerStats.Experience + "/" + playerStats.LevelUpExp + ")";
        Transform PlayerStatValues = statsInfo.transform.GetChild(3);

        PlayerStatValues.transform.GetChild(1).GetComponent<Text>().text = playerStats.Health + "/" + (playerStats.MaxHealth + playerStats.MaxHealthBonus);
        PlayerStatValues.transform.GetChild(2).GetComponent<Text>().text = playerStats.Mana + "/" + (playerStats.MaxMana + playerStats.MaxManaBonus);
        UpdateStatInfo();
    }

    public void UpdateStatInfo()
    {
        Transform PlayerStatValues = statsInfo.transform.GetChild(3);

        PlayerStatValues.transform.GetChild(0).GetComponent<Text>().text = playerStats.Gold.ToString();
        PlayerStatValues.transform.GetChild(1).GetComponent<Text>().text = playerStats.Health + "/" + (playerStats.MaxHealth + playerStats.MaxHealthBonus);
        PlayerStatValues.transform.GetChild(2).GetComponent<Text>().text = playerStats.Mana + "/" + (playerStats.MaxMana + playerStats.MaxManaBonus);

        Text strB = PlayerStatValues.transform.GetChild(3).GetComponent<Text>();

        if (playerStats.StrBonus > 0)
        {
            strB.text = (playerStats.Strength + playerStats.StrBonus) + " <color=#00ff00ff>(+" + playerStats.StrBonus + ")</color>";
        }
        else if (playerStats.StrBonus < 0)
        {
            strB.text = (playerStats.Strength + playerStats.StrBonus) + " <color=#ff0000ff>(" + playerStats.StrBonus + ")</color>";
        }
        else
        {
            strB.text = playerStats.Strength.ToString();
        }

        Text dexB = PlayerStatValues.transform.GetChild(4).GetComponent<Text>();

        if (playerStats.DexBonus > 0)
        {
            dexB.text = (playerStats.Dexterity + playerStats.DexBonus) + " <color=#00ff00ff>(+" + playerStats.DexBonus + ")</color>";
        }
        else if (playerStats.DexBonus < 0)
        {
            dexB.text = (playerStats.Dexterity + playerStats.DexBonus) + " <color=#ff0000ff>(" + playerStats.DexBonus + ")</color>";
        }
        else
        {
            dexB.text = playerStats.Dexterity.ToString();
        }

        Text intB = PlayerStatValues.transform.GetChild(5).GetComponent<Text>();

        if (playerStats.IntBonus > 0)
        {
            intB.text = (playerStats.Intelligence + playerStats.IntBonus) + " <color=#00ff00ff>(+" + playerStats.IntBonus + ")</color>";
        }
        else if (playerStats.IntBonus < 0)
        {
            intB.text = (playerStats.Intelligence + playerStats.IntBonus) + " <color=#ff0000ff>(" + playerStats.IntBonus + ")</color>";
        }
        else
        {
            intB.text = playerStats.Intelligence.ToString();
        }

        Text atkB = PlayerStatValues.transform.GetChild(6).GetComponent<Text>();

        if (playerStats.AtkBonus > 0)
        {
            atkB.text = playerStats.AttackDamage + " <color=#00ff00ff>(+" + playerStats.AtkBonus + ")</color>";
        }
        else if (playerStats.AtkBonus < 0)
        {
            atkB.text = playerStats.AttackDamage + " <color=#ff0000ff>(" + playerStats.AtkBonus + ")</color>";
        }
        else
        {
            atkB.text = playerStats.AttackDamage.ToString();
        }

        Text armB = PlayerStatValues.transform.GetChild(7).GetComponent<Text>();

        if (playerStats.ArmBonus > 0)
        {
            armB.text = playerStats.Armor + " <color=#00ff00ff>(+" + playerStats.ArmBonus + ")</color>";
        }
        else if (playerStats.ArmBonus < 0)
        {
            armB.text = playerStats.Armor + " <color=#ff0000ff>(" + playerStats.ArmBonus + ")</color>";
        }
        else
        {
            armB.text = playerStats.Armor.ToString();
        }

        PlayerStatValues.GetChild(8).GetComponent<Text>().text = playerStats.FireDamage + " Dmg / " + playerStats.FireResistance + " Res";
        PlayerStatValues.GetChild(9).GetComponent<Text>().text = playerStats.WaterDamage + " Dmg / " + playerStats.WaterResistance + " Res";
        PlayerStatValues.GetChild(10).GetComponent<Text>().text = playerStats.EarthDamage + " Dmg / " + playerStats.EarthResistance + " Res";
        PlayerStatValues.GetChild(11).GetComponent<Text>().text = playerStats.WindDamage + " Dmg / " + playerStats.WindDamage + " Res";
    }

    public void MoveMarker(string direction)
    {
        switch (direction)
        {
            case "Left":

                if (invMarker.CurrentSlot.Col - 1 >= 0)
                {
                    invMarker.Move(invMarker.CurrentInv[invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col - 1]);
                }
                else
                {
                    if (invMarker.CurrentInv == weaponGemEquipment)
                    {
                        invMarker.UpdateCurrentInv(backpack, bpUI, backpack[invMarker.CurrentSlot.Row, 3]);   
                    }
                    else if (invMarker.CurrentInv == armorGemEquipment)
                    {
                        invMarker.UpdateCurrentInv(backpack, bpUI, backpack[invMarker.CurrentSlot.Row + 2, 3]);
                    }
                }

            break;

            case "Up":

                if (invMarker.CurrentSlot.Row - 1 >= 0)
                {
                    invMarker.Move(invMarker.CurrentInv[invMarker.CurrentSlot.Row - 1, invMarker.CurrentSlot.Col]);
                }
                else
                {
                    if (invMarker.CurrentInv == backpack)
                    {
                        invMarker.Move(invMarker.CurrentInv[invMarker.CurrentInv.GetLength(0) - 1, invMarker.CurrentSlot.Col]);
                    }
                    else if (invMarker.CurrentInv == armorGemEquipment)
                    {
                        invMarker.UpdateCurrentInv(weaponGemEquipment, wgUI, weaponGemEquipment[1, invMarker.CurrentSlot.Col]);
                    }
                    else
                    {
                        invMarker.UpdateCurrentInv(armorGemEquipment, agUI, armorGemEquipment[1, invMarker.CurrentSlot.Col]);
                    }
                    
                }

            break;

            case "Right":

                if (invMarker.CurrentSlot.Col + 1 < invMarker.CurrentInv.GetLength(0))
                {
                    invMarker.Move(invMarker.CurrentInv[invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col + 1]);
                }
                else
                {
                    if (invMarker.CurrentInv == backpack)
                    {
                        if (invMarker.CurrentSlot.Row <= (backpack.GetLength(0) / 2 - 1))
                        {
                            invMarker.UpdateCurrentInv(weaponGemEquipment, wgUI, weaponGemEquipment[invMarker.CurrentSlot.Row, 0]);
                        }
                        else
                        {
                            invMarker.UpdateCurrentInv(armorGemEquipment, agUI, armorGemEquipment[invMarker.CurrentSlot.Row - 2, 0]);
                        }
                    }
                }

            break;

            case "Down":

                if (invMarker.CurrentSlot.Row + 1 < invMarker.CurrentInv.GetLength(1))
                {
                    invMarker.Move(invMarker.CurrentInv[invMarker.CurrentSlot.Row + 1, invMarker.CurrentSlot.Col]);
                }
                else
                {   
                    if (invMarker.CurrentInv == backpack)
                    {
                        invMarker.Move(invMarker.CurrentInv[0, invMarker.CurrentSlot.Col]);
                    }
                    else if (invMarker.CurrentInv == armorGemEquipment)
                    {
                        invMarker.UpdateCurrentInv(weaponGemEquipment, wgUI, weaponGemEquipment[0, invMarker.CurrentSlot.Col]);
                    }
                    else
                    {
                        invMarker.UpdateCurrentInv(armorGemEquipment, agUI, armorGemEquipment[0, invMarker.CurrentSlot.Col]);
                    }          
                }

            break;
        }

        if (invMarker.CurrentSlot.StoredItem != null)
        {
            GenerateTooltip(false);
        }
        else
        {
            GenerateTooltip(true);
        }
        
    }

    private void GenerateTooltip(bool isEmpty)
    {
        Text itemName = itemInfo.transform.GetChild(0).GetComponent<Text>();
        Text itemTier = itemInfo.transform.GetChild(1).GetComponent<Text>();
        Text itemHeader = itemInfo.transform.GetChild(2).GetComponent<Text>();
        Text itemDesc = itemInfo.transform.GetChild(3).GetComponent<Text>();
        Image windowBorder = itemInfo.transform.GetChild(6).GetComponent<Image>();

        List<Text> statList = new List<Text>();
        Dictionary<string, float> foundStats = new Dictionary<string, float>();

        for (int i = 0; i < 5; i++)
        {
            statList.Add(itemInfo.transform.GetChild(4).transform.GetChild(i).GetComponent<Text>());
        }

        for (int i = 0; i < 5; i++)
        {
            statList.Add(itemInfo.transform.GetChild(5).transform.GetChild(i).GetComponent<Text>());
        }

        Color propertyColor;

        if (!isEmpty)
        {
            I_item currentItem = invMarker.CurrentSlot.StoredItem;
            itemName.text = currentItem.Name;
            itemDesc.text = currentItem.Description;
            itemTier.text = "Tier " + currentItem.Tier;
            itemHeader.text = "[" + currentItem.HeaderDescription + "]";

            switch (currentItem.Tier)
            {
                case 1:

                    propertyColor = new Color(0, 255, 0);
                    itemTier.color = propertyColor;
                    windowBorder.color = propertyColor;

                break;

                case 2:

                    propertyColor = new Color(0, 255, 255);
                    itemTier.color = propertyColor;
                    windowBorder.color = propertyColor;

                break;
            }

            switch (GetItemType(currentItem))
            {
                case "ArmorGem":

                    propertyColor = new Color(0, 0.3656104f, 1);
                    itemHeader.color = propertyColor;

                    for (int i = 0; i < statList.Count; i++)
                    {
                        statList[i].text = null;
                    }

                    ArmorGem ag = (ArmorGem)currentItem;
                    foundStats = ag.GetStats();
                    int statIndex = 0;

                    foreach(KeyValuePair<string, float> stat in foundStats)
                    {
                        if (stat.Value > 0)
                        {
                            statList[statIndex].text = "+ " + stat.Value + " " + stat.Key;
                            statIndex++;
                        } 
                        else if (stat.Value < 0)
                        {
                            statList[statIndex].text = "- " + Mathf.Abs(stat.Value) + " " + stat.Key;
                            statIndex++;
                        }
                        else
                        {
                            statList[statIndex].text = null;
                        }
                    }

                break;

                case "WeaponGem":

                    propertyColor = new Color(1, 0, 0.2169275f);
                    itemHeader.color = propertyColor;

                    for (int i = 0; i < statList.Count; i++)
                    {
                        statList[i].text = null;
                    }

                    WeaponGem wg = (WeaponGem)currentItem;
                    foundStats = wg.GetStats();
                    statIndex = 0;

                    foreach (KeyValuePair<string, float> stat in foundStats)
                    {
                        if (stat.Value > 0)
                        {
                            statList[statIndex].text = "+ " + stat.Value + " " + stat.Key;
                            statIndex++;
                        }
                        else if (stat.Value < 0)
                        {
                            statList[statIndex].text = "- " + Mathf.Abs(stat.Value) + " " + stat.Key;
                            statIndex++;
                        }
                        else
                        {
                            statList[statIndex].text = null;
                        }
                    }

                break;

                case "SimplePotion":

                    propertyColor = new Color(1, 0, 1);
                    itemHeader.color = propertyColor;

                    for (int i = 0; i < statList.Count; i++)
                    {
                        statList[i].text = null;
                    }

                    SimplePotion sp = (SimplePotion)currentItem;
                    foundStats = sp.GetStats();
                    statIndex = 0;

                    foreach (KeyValuePair<string, float> stat in foundStats)
                    {
                        if (stat.Key == "Recovery time")
                        {
                            statList[statIndex].text = stat.Value + "s " + stat.Key;
                        }
                        else
                        {
                            if (stat.Value > 0)
                            {
                                statList[statIndex].text = "+ " + stat.Value + " " + stat.Key;
                                statIndex++;
                            }
                            else if (stat.Value < 0)
                            {
                                statList[statIndex].text = "- " + Mathf.Abs(stat.Value) + " " + stat.Key;
                                statIndex++;
                            }
                            else
                            {
                                statList[statIndex].text = null;
                            }
                        }        
                    }

                break;
            }
        }
        else
        {
            itemName.text = null;
            itemTier.text = null;
            itemHeader.text = null;
            itemDesc.text = null;
            windowBorder.color = Color.white;

            for (int i = 0; i < statList.Count; i++)
            {
                statList[i].text = null;
            }
        }
    }

    private string GetItemType(I_item item)
    {
        string itemType = null;

        if (item.GetType() == typeof(ArmorGem))
        {
            itemType = "ArmorGem";
        }
        else if (item.GetType() == typeof(WeaponGem))
        {
            itemType = "WeaponGem";
        }
        else if (item.GetType() == typeof(SimplePotion))
        {
            itemType = "SimplePotion";
        }

        return itemType;
    }

    public void SelectOnSlot()
    {
        if (!invMarker.HasMarkedSlot)
        {
            if (invMarker.CurrentSlot.StoredItem != null)
            {
                invMarker.MarkSlot(invMarker.CurrentSlot);
            }
        }
        else
        {
            if (invMarker.CurrentSlot != invMarker.MarkedSlot)
            {
                string slotItemType = GetItemType(invMarker.MarkedSlot.StoredItem);

                switch (slotItemType)
                {
                    case "ArmorGem":

                        if (invMarker.CurrentInv == weaponGemEquipment)
                        {
                            Debug.Log("You cannot equip that here");
                            invMarker.UnmarkSlot();
                            GenerateTooltip(true);
                        }
                        else if (invMarker.CurrentInv == armorGemEquipment)
                        {
                            MoveAndEquipItem();
                            GenerateTooltip(false);
                        }
                        else if (invMarker.MarkedSlot.CurrentInvUI == bpUI)
                        {
                            MoveItem();
                            GenerateTooltip(false);
                        }
                        else
                        {
                            MoveAndUnequipItem();
                            GenerateTooltip(false);
                        }

                    break;

                    case "WeaponGem":

                        if (invMarker.CurrentInv == armorGemEquipment)
                        {
                            Debug.Log("You cannot equip that here");
                            invMarker.UnmarkSlot();
                            GenerateTooltip(true);
                        }
                        else if (invMarker.CurrentInv == weaponGemEquipment)
                        {
                            MoveAndEquipItem();
                            GenerateTooltip(false);
                        }
                        else if (invMarker.MarkedSlot.CurrentInvUI == bpUI)
                        {
                            MoveItem();
                            GenerateTooltip(false);
                        }
                        else
                        {
                            MoveAndUnequipItem();
                            GenerateTooltip(false);
                        }

                    break;

                    case "SimplePotion":

                        if (invMarker.CurrentInv == backpack)
                        {
                            MoveItem();
                            GenerateTooltip(false);
                        }
                        else
                        {
                            Debug.Log("You cannot equip a consumable");
                            invMarker.UnmarkSlot();
                            GenerateTooltip(true);
                        } 

                    break;   
                }
            }
            else
            {
                invMarker.UnmarkSlot();
            }
        }
    }

    public void UseOnSlot()
    {
        if (!invMarker.HasMarkedSlot)
        {
            if (invMarker.CurrentSlot.StoredItem != null)
            {
                string slotItemType = GetItemType(invMarker.CurrentSlot.StoredItem);

                switch (slotItemType)
                {
                    case "ArmorGem":

                        if (invMarker.CurrentInv == backpack)
                        {
                            Slot possibleSlot = SearchForSlot(armorGemEquipment);

                            if (possibleSlot != null)
                            {
                                MoveAndEquipItem(possibleSlot);
                                GenerateTooltip(true);
                            }
                        }
                        else
                        {
                            Slot possibleSlot = SearchForSlot(backpack);

                            if (possibleSlot != null)
                            {
                                MoveAndUnequipItem(possibleSlot);
                                GenerateTooltip(true);
                            }
                        }

                        break;

                    case "WeaponGem":

                        if (invMarker.CurrentInv == backpack)
                        {
                            Slot possibleSlot = SearchForSlot(weaponGemEquipment);

                            if (possibleSlot != null)
                            {
                                MoveAndEquipItem(possibleSlot);
                                GenerateTooltip(true);
                            }
                        }
                        else
                        {
                            Slot possibleSlot = SearchForSlot(backpack);

                            if (possibleSlot != null)
                            {
                                MoveAndUnequipItem(possibleSlot);
                                GenerateTooltip(true);
                            }
                        }

                        break;

                    case "SimplePotion":

                        SimplePotion sp = (SimplePotion)invMarker.CurrentSlot.StoredItem;
                        sp.Consume(player.GetComponent<Player>());
                        GenerateTooltip(true);

                        break;
                }
            }
        }
        else
        {
            invMarker.UnmarkSlot();
        }
    }

    public void Cancel()
    {
        if (invMarker.HasMarkedSlot)
        {
            invMarker.UnmarkSlot();
        }
    }

    private void MoveItem()
    {
        if (invMarker.CurrentSlot.StoredItem == null)
        {
            invMarker.CurrentSlot.StoredItem = invMarker.MarkedSlot.StoredItem;
            invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col, invMarker.MarkedSlot.StoredItem);
            invMarker.MarkedSlot.StoredItem = null;
            invMarker.MarkedInvUI.UpdateInv(invMarker.MarkedSlot.Row, invMarker.MarkedSlot.Col);
            invMarker.UnmarkSlot();
        }
        else
        {
            I_item itemDummy = invMarker.CurrentSlot.StoredItem;
            invMarker.CurrentSlot.StoredItem = invMarker.MarkedSlot.StoredItem;
            invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col, invMarker.MarkedSlot.StoredItem);
            invMarker.MarkedSlot.StoredItem = null;
            invMarker.MarkedSlot.StoredItem = itemDummy;
            invMarker.MarkedInvUI.UpdateInv(invMarker.MarkedSlot.Row, invMarker.MarkedSlot.Col, itemDummy);
            invMarker.UnmarkSlot();
        }
    }

    private void MoveAndEquipItem()
    {
        if (invMarker.CurrentSlot.StoredItem == null)
        {
            EquipItem(invMarker.MarkedSlot.StoredItem);
            invMarker.CurrentSlot.StoredItem = invMarker.MarkedSlot.StoredItem;        
            invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col, invMarker.MarkedSlot.StoredItem);
            invMarker.MarkedSlot.StoredItem = null;
            invMarker.MarkedInvUI.UpdateInv(invMarker.MarkedSlot.Row, invMarker.MarkedSlot.Col);
            invMarker.UnmarkSlot();
        }
        else
        {
            I_item itemDummy = invMarker.CurrentSlot.StoredItem;
            invMarker.CurrentSlot.StoredItem = invMarker.MarkedSlot.StoredItem;
            EquipItem(invMarker.MarkedSlot.StoredItem);
            invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col, invMarker.MarkedSlot.StoredItem);
            invMarker.MarkedSlot.StoredItem = null;
            UnequipItem(itemDummy);
            invMarker.MarkedSlot.StoredItem = itemDummy;
            invMarker.MarkedInvUI.UpdateInv(invMarker.MarkedSlot.Row, invMarker.MarkedSlot.Col, itemDummy);
            invMarker.UnmarkSlot();
        }
    }

    private void MoveAndEquipItem(Slot destiny)
    {
        EquipItem(invMarker.CurrentSlot.StoredItem);
        destiny.StoredItem = invMarker.CurrentSlot.StoredItem;
        destiny.CurrentInvUI.UpdateInv(destiny.Row, destiny.Col, invMarker.CurrentSlot.StoredItem);
        invMarker.CurrentSlot.StoredItem = null;
        invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col);
    }

    private void MoveAndUnequipItem()
    {
        if (invMarker.CurrentSlot.StoredItem == null)
        {
            UnequipItem(invMarker.MarkedSlot.StoredItem);
            invMarker.CurrentSlot.StoredItem = invMarker.MarkedSlot.StoredItem;
            invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col, invMarker.MarkedSlot.StoredItem);
            invMarker.MarkedSlot.StoredItem = null;
            invMarker.MarkedInvUI.UpdateInv(invMarker.MarkedSlot.Row, invMarker.MarkedSlot.Col);
            invMarker.UnmarkSlot();
        }
        else
        {
            I_item itemDummy = invMarker.CurrentSlot.StoredItem;
            invMarker.CurrentSlot.StoredItem = invMarker.MarkedSlot.StoredItem;
            UnequipItem(invMarker.MarkedSlot.StoredItem);
            invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col, invMarker.MarkedSlot.StoredItem);
            invMarker.MarkedSlot.StoredItem = null;
            EquipItem(itemDummy);
            invMarker.MarkedSlot.StoredItem = itemDummy;
            invMarker.MarkedInvUI.UpdateInv(invMarker.MarkedSlot.Row, invMarker.MarkedSlot.Col, itemDummy);
            invMarker.UnmarkSlot();
        }
    }

    private void MoveAndUnequipItem(Slot destiny)
    {
        UnequipItem(invMarker.CurrentSlot.StoredItem);
        destiny.StoredItem = invMarker.CurrentSlot.StoredItem;
        destiny.CurrentInvUI.UpdateInv(destiny.Row, destiny.Col, invMarker.CurrentSlot.StoredItem);
        invMarker.CurrentSlot.StoredItem = null;
        invMarker.CurrentSlot.CurrentInvUI.UpdateInv(invMarker.CurrentSlot.Row, invMarker.CurrentSlot.Col);
    }

    private void EquipItem(I_item itemToEquip)
    {
        switch(GetItemType(itemToEquip))
        {
            case "ArmorGem":

                ArmorGem ag = (ArmorGem)itemToEquip;
                playerStats.ArmBonus += ag.ArmBonus;
                playerStats.HealthRegen += ag.HpRegenBonus;
                playerStats.ManaRegen += ag.MpRegenBonus;
                playerStats.StrBonus += ag.StrBonus;
                playerStats.DexBonus += ag.DexBonus;
                playerStats.IntBonus += ag.IntBonus;
                playerStats.FireResistance += ag.FireResBonus;
                playerStats.WaterResistance += ag.WaterResBonus;
                playerStats.EarthResistance += ag.EarthResBonus;
                playerStats.WindResistance += ag.WindResBonus;

            break;

            case "WeaponGem":

                WeaponGem wp = (WeaponGem)itemToEquip;
                playerStats.ChargePower += wp.ChargePwrBonus;
                playerStats.AtkBonus += wp.AtkDmgBonus;
                playerStats.StrBonus += wp.StrBonus;
                playerStats.DexBonus += wp.DexBonus;
                playerStats.IntBonus += wp.IntBonus;
                playerStats.FireDamage += wp.FireDmgBonus;
                playerStats.WaterDamage += wp.WaterDmgBonus;
                playerStats.EarthDamage += wp.EarthDmgBonus;
                playerStats.WindDamage += wp.WindDmgBonus;

            break;
        }

        playerStats.UpdateBaseStats();
        UpdateStatInfo();
    }

    private void UnequipItem(I_item itemToUnequip)
    {
        switch (GetItemType(itemToUnequip))
        {
            case "ArmorGem":

                ArmorGem ag = (ArmorGem)itemToUnequip;
                playerStats.ArmBonus -= ag.ArmBonus;
                playerStats.HealthRegen -= ag.HpRegenBonus;
                playerStats.ManaRegen -= ag.MpRegenBonus;
                playerStats.StrBonus -= ag.StrBonus;
                playerStats.DexBonus -= ag.DexBonus;
                playerStats.IntBonus -= ag.IntBonus;
                playerStats.FireResistance -= ag.FireResBonus;
                playerStats.WaterResistance -= ag.WaterResBonus;
                playerStats.EarthResistance -= ag.EarthResBonus;
                playerStats.WindResistance -= ag.WindResBonus;

                break;

            case "WeaponGem":

                WeaponGem wp = (WeaponGem)itemToUnequip;
                playerStats.ChargePower -= wp.ChargePwrBonus;
                playerStats.AtkBonus -= wp.AtkDmgBonus;
                playerStats.StrBonus -= wp.StrBonus;
                playerStats.DexBonus -= wp.DexBonus;
                playerStats.IntBonus -= wp.IntBonus;
                playerStats.FireDamage -= wp.FireDmgBonus;
                playerStats.WaterDamage -= wp.WaterDmgBonus;
                playerStats.EarthDamage -= wp.EarthDmgBonus;
                playerStats.WindDamage -= wp.WindDmgBonus;

                break;
        }

        playerStats.UpdateBaseStats();
        UpdateStatInfo();
    }

    public bool AddItemToInventory(GameObject itemToAdd)
    {
        I_item item;
        bool itemAdded;
        Slot possibleSlot = SearchForSlot(backpack);

        if (possibleSlot != null)
        {
            item = itemToAdd.GetComponent<ItemInstance>().GetInstance();

            backpack[possibleSlot.Row, possibleSlot.Col].StoredItem = item;
            bpUI.UpdateInv(possibleSlot.Row, possibleSlot.Col, item);
            itemAdded = true;

            if (possibleSlot == invMarker.CurrentSlot)
            {
                GenerateTooltip(false);
            }
        }
        else
        {
            Debug.Log("Invetory is full");
            itemAdded = false;
        }

        return itemAdded;
    }

    private Slot SearchForSlot(Slot[,] invToSearch)
    {
        for (int i = 0; i < invToSearch.GetLength(0); i++)
        {
            for (int j = 0; j < invToSearch.GetLength(1); j++)
            {
                if (invToSearch[i, j].StoredItem == null)
                {
                    return invToSearch[i, j];
                }
            }
        }

        return null;
    }
}

class Slot
{
    private I_item storedItem;
    private InventoryUI currentInvUI;

    private int row;
    private int col;

    public Slot(int _row, int _col)
    {
        row = _row;
        col = _col;
    }

    public I_item StoredItem { get { return storedItem; } set { storedItem = value; } }
    public InventoryUI CurrentInvUI { get { return currentInvUI; } set { currentInvUI = value; } }
    public int Row { get { return row; } set { row = value; } }
    public int Col { get { return col; } set { col = value; } }
}

class Marker
{
    private Slot currentSlot;
    private Slot markedSlot;
    private Slot[,] currentInv;
    private Slot[,] markedInv;
    private InventoryUI markedInvUI;

    private bool hasMarkedSlot;

    public Slot CurrentSlot { get {return currentSlot; } }
    public Slot[,] CurrentInv { get { return currentInv; } }
    public Slot MarkedSlot { get { return markedSlot; } }
    public InventoryUI MarkedInvUI { get { return markedInvUI; } }

    public bool HasMarkedSlot { get { return hasMarkedSlot; } }

    public Marker(Slot[,] _starterInv)
    {
        currentInv = _starterInv;
        currentSlot = _starterInv[0, 0];
    }

    public void UpdateCurrentInv(Slot[,] newInv, InventoryUI newInvUI, Slot newSlot)
    {
        currentInv = newInv;
        currentSlot.CurrentInvUI.UpdateMarker();
        currentSlot = newSlot;
        newSlot.CurrentInvUI.UpdateMarker(newSlot.Row, newSlot.Col);
    }

    public void UpdatePos(Slot newSlot)
    {
        currentSlot = newSlot;
    }

    public void Move(Slot newSlot)
    {
        currentSlot = newSlot;
        currentSlot.CurrentInvUI.UpdateMarker(currentSlot.Row, currentSlot.Col);
    }

    public void MarkSlot(Slot newSlot)
    {
        markedSlot = newSlot;
        markedInv = currentInv;
        hasMarkedSlot = true;
        currentSlot.CurrentInvUI.UpdateMarkedSlot(markedSlot.Row, markedSlot.Col);
        markedInvUI = currentSlot.CurrentInvUI;
    }

    public void UnmarkSlot()
    {
        markedInv = null;
        hasMarkedSlot = false;
        MarkedInvUI.UpdateMarkedSlot();
        markedInvUI = null;
    }
}

class InventoryUI
{
    private GameObject[,] invSlots;
    private GameObject[,] slotItems;
    private GameObject refInv;
    private GameObject invTab;
    private GridLayoutGroup grid;
    private Vector2 slotSize;

    private GameObject slotMarker;
    private GameObject selectedSlotMarker;

    private bool hasMarker = false;
    private bool hasSelectedSlot = false;
    
    public InventoryUI(GameObject _invTab,GameObject _refInv, bool _hasMarker)
    {
        refInv = _refInv;
        invTab = _invTab;
        grid = refInv.GetComponent<GridLayoutGroup>();
        hasMarker = _hasMarker;
    }

    public void GenerateInvUI(int row, int col, GameObject slotSprite, GameObject slotMarkerSprite, GameObject selectedSlotMarkerSprite)
    {
        slotSize = new Vector2(refInv.GetComponent<RectTransform>().rect.width / col - grid.spacing.x, refInv.GetComponent<RectTransform>().rect.height / row - grid.spacing.y);
        grid.cellSize = slotSize;
        invSlots = new GameObject[row, col];
        slotItems = new GameObject[row, col];

        for (int i = 0; i < (row); i++)
        {
            for (int j = 0; j < (col); j++)
            {
                GameObject slot = GameObject.Instantiate(slotSprite);
                slot.transform.SetParent(refInv.transform);
                invSlots[i, j] = slot;

                GameObject newItem = new GameObject("SlotItem");
                newItem.AddComponent<Image>();
                slotItems[i, j] = newItem;
                slotItems[i, j].GetComponent<RectTransform>().sizeDelta = slotSize / 1.5f;
                slotItems[i, j].transform.SetParent(invSlots[i, j].transform);
                slotItems[i, j].GetComponent<Image>().enabled = false;
            }
        }

        GameObject newMarker = GameObject.Instantiate(slotMarkerSprite);
        GameObject newSlotMark = GameObject.Instantiate(selectedSlotMarkerSprite);

        slotMarker = newMarker;
        selectedSlotMarker = newSlotMark;

        if (!hasMarker)
        {
            slotMarker.SetActive(false);
        }

        selectedSlotMarker.SetActive(false);
        slotMarker.GetComponent<RectTransform>().sizeDelta = slotSize;
        selectedSlotMarker.GetComponent<RectTransform>().sizeDelta = slotSize;
        slotMarker.transform.SetParent(invSlots[0, 0].transform);
        selectedSlotMarker.transform.SetParent(invSlots[0, 0].transform);
    }

    public void UpdateInv(int row, int col, I_item itemToAdd)
    {
        slotItems[row, col].GetComponent<Image>().sprite = itemToAdd.MySprite;
        slotItems[row, col].transform.SetParent(invSlots[row, col].transform);
        slotItems[row, col].transform.position = invSlots[row, col].transform.position;
        slotItems[row, col].GetComponent<Image>().enabled = true;
    }

    public void UpdateInv(int row, int col)
    {
        slotItems[row, col].GetComponent<Image>().enabled = false;
    }

    public void UpdateMarker(int row, int col)
    {
        slotMarker.SetActive(true);
        slotMarker.transform.SetParent(invSlots[row, col].transform);
        slotMarker.transform.position = invSlots[row, col].transform.position;
    }

    public void UpdateMarker()
    {
        slotMarker.SetActive(false);
    }

    public void UpdateMarkedSlot(int row, int col)
    {
        selectedSlotMarker.SetActive(true);
        selectedSlotMarker.transform.SetParent(invSlots[row, col].transform);
        selectedSlotMarker.transform.position = invSlots[row, col].transform.position;
    }

    public void UpdateMarkedSlot()
    {
        selectedSlotMarker.SetActive(false);
    }
}