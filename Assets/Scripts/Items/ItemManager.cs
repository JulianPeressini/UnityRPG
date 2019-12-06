using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct drops
{
    public string[] tierDrops;
}

public class ItemManager : MonoBehaviour
{
    static private ItemManager instance;
    static public ItemManager Instance { get { return instance; } }
    [SerializeField] private drops[] weaponList;
    [SerializeField] private drops[] armorList;
    [SerializeField] private drops[] simplePotionList;

    private FileType itemType;
    private string itemName;
    private bool questRelated;

    //[SerializeField] private 

    [SerializeField] private Sprite droppedItemSprite;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AttemptDrop(1, transform.position);
        }
    }

    public void AttemptDrop(int tier, Vector3 itemLocation)
    {
        int dropChance = Random.Range(0, 100);
        int rmdItem;
        tier -= 1;

        if (dropChance > 50)
        {
            int rmdType = Random.Range(0, 2);

            switch (rmdType)
            {
                case 0:

                    itemType = FileType.WeaponGem;
                    itemName = weaponList[tier].tierDrops[Random.Range(0, weaponList[tier].tierDrops.Length)];

                break;

                case 1:

                    itemType = FileType.SimplePotion;
                    itemName = simplePotionList[tier].tierDrops[Random.Range(0, simplePotionList[tier].tierDrops.Length)];

                break;

                case 2:

                    itemType = FileType.ArmorGem;
                    itemName = armorList[tier].tierDrops[Random.Range(0, armorList[tier].tierDrops.Length)];

                break;
            }

            if (QuestManager.Instance.FindRelevancy(itemName) != null)
            {
                questRelated = true;
            }
            else
            {
                questRelated = false;
            }

            Generate(itemType, itemName, itemLocation, questRelated);
        }
    }

    public void Generate(FileType type, string itemName, Vector3 itemLocation, bool questItem)
    {
        GameObject item = new GameObject();
        string path = Application.dataPath;
        item.AddComponent<ItemInstance>();
        string itemTag = "normalItem";

        if (questItem)
        {
            itemTag = "questItem";
        }

        switch (type)
        {
            case FileType.SimplePotion:
                path += "/Resources/Json/Consumable/Simple Potion/" + itemName + ".Json";
                item.GetComponent<ItemInstance>().SetInstanceStats(JsonUtility.FromJson<SimplePotion>(File.ReadAllText(path)));
                item.name = "DroppedSimplePotion";
                item.tag = itemTag;
                break;

            case FileType.WeaponGem:
                path += "/Resources/Json/Equipment/Weapon Gem/" + itemName + ".Json";
                item.GetComponent<ItemInstance>().SetInstanceStats(JsonUtility.FromJson<WeaponGem>(File.ReadAllText(path)));
                item.name = "DropppedWeaponGem";
                item.tag = itemTag;
                break;

            case FileType.ArmorGem:
                path += "/Resources/Json/Equipment/Armor Gem/" + itemName + ".Json";
                item.GetComponent<ItemInstance>().SetInstanceStats(JsonUtility.FromJson<ArmorGem>(File.ReadAllText(path)));
                item.name = "DropppedArmorGem";
                item.tag = itemTag;
                break;

            default:

                break;
        }

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = droppedItemSprite;
        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;
        item.AddComponent<DroppedItem>();

        item.transform.position = itemLocation;
    }
}