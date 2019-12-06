using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    I_item itemInstance;

    public I_item GetInstance()
    {
        return itemInstance;
    }

    public void SetInstanceStats(I_item stats)
    {
        itemInstance = stats;
        itemInstance.SetSprite();
    }
}
