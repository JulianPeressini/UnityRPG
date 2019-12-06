using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        bool pickUpSuccesful;

        if (other.transform.tag == "Player")
        {
            pickUpSuccesful = Inventory.Instance.AddItemToInventory(gameObject);

            if (pickUpSuccesful)
            {
                if (gameObject.tag == "questItem")
                {
                    QuestManager.Instance.ItemAquired(gameObject.GetComponent<ItemInstance>().GetInstance());
                }

                Destroy(gameObject);
            }
        }
    }
}
