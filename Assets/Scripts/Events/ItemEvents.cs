using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvents : MonoBehaviour
{
    public delegate void ItemEventHandler(string itemName);
    public static event ItemEventHandler OnItemCollect;

    public static void ItemCollected(string itemName)
    {
        if (OnItemCollect != null)
            OnItemCollect(itemName);
    }
}
