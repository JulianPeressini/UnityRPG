using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum FileType
{
    SimplePotion,
    WeaponGem,
    ArmorGem

}

[System.Serializable]
public class JsonCreator : MonoBehaviour
{
    public FileType fType;

    public SimplePotion jsonSimplePotion;
    public WeaponGem jsonWeaponGem;
    public ArmorGem jsonArmorGem;

    public string VerifyJSONCreation()
    {
        return p_message;
    }

    string p_message;

    public void CreateJSON()
    {
        string path = Application.dataPath;
        string jsonString;

        switch (fType)
        {

            case FileType.SimplePotion:
                jsonString = JsonUtility.ToJson(jsonSimplePotion);
                p_message = "Simple potion Creation OK";
                path += "/Resources/Json/Consumable/Simple Potion/" + jsonSimplePotion.Name + ".Json";
                break;

            case FileType.WeaponGem:
                jsonString = JsonUtility.ToJson(jsonWeaponGem);
                p_message = "Weapon Gem Creation OK";
                path += "/Resources/Json/Equipment/Weapon Gem/" + jsonWeaponGem.Name + ".Json";
                break;

            case FileType.ArmorGem:
                jsonString = JsonUtility.ToJson(jsonArmorGem);
                p_message = "Armor Gem Creation OK";
                path += "/Resources/Json/Equipment/Armor Gem/" + jsonArmorGem.Name + ".Json";
                break;

            default:
                p_message = "Could not create";
                jsonString = "Fatal Error";
                path += "/Resources/Json/Corrupted/failure.Json";
                break;
        }

        if (!File.Exists(path))
        {
            File.WriteAllText(path, jsonString);
        }
    }

}
