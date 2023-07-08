using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
 public static ItemController Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        ItemEvents.OnItemCollect += CollectedItem;
        DontDestroyOnLoad(this);
    }

    private void CollectedItem(string itemName)
    {
      Debug.Log("COLETOU O ITEM");
      PlayerController.Instance.healPlayer();
    }
}
