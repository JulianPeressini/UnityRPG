using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class I_item
{
    //Item
    [SerializeField] private int itemID;
    [SerializeField] private string name;
    [SerializeField] private string headerDescription;
    [SerializeField] private string description;
    [SerializeField] private int tier;
    private Sprite mySprite;
    

    //Public
    public int ItemID { get { return itemID; } set { itemID = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string HeaderDescription { get { return headerDescription; } set { headerDescription= value; } }
    public string Description { get { return description; } set { description = value; } }
    public int Tier { get { return tier; } set { tier = value; } }
    public Sprite MySprite { get { return mySprite; } }


    //Meta
    public void SetSprite()
    {
        mySprite = Resources.Load<Sprite>("Sprites/Items/" + name);

        if (mySprite == null)
        {
            Debug.Log("Sprite not found");
        }
    }
}
