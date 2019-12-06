using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class JsonCreatorGUI : MonoBehaviour
{
    [CustomEditor(typeof(JsonCreator))]
    public class JsonCreatorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            JsonCreator myScript = (JsonCreator)target;
            SerializedObject serializedObj = new SerializedObject(myScript);

            switch (myScript.fType)
            {
                case FileType.SimplePotion:
                    DrawPropertiesExcluding(serializedObj, new string[] { "jsonWeaponGem", "jsonArmorGem" });
                    break;

                case FileType.WeaponGem:
                    DrawPropertiesExcluding(serializedObj, new string[] { "jsonSimplePotion", "jsonArmorGem" });
                    break;

                case FileType.ArmorGem:
                    DrawPropertiesExcluding(serializedObj, new string[] { "jsonSimplePotion", "jsonWeaponGem" });
                    break;

                default:
                    break;
            }

            if (GUILayout.Button("Create JSON"))
            {
                myScript.CreateJSON();
            }

            GUILayout.Label(myScript.VerifyJSONCreation());
            serializedObj.ApplyModifiedProperties();
        }
    }
}
