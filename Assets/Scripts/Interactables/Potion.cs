using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    private bool isEnabled = true;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && isEnabled)
        {
            isEnabled = false;
            Destroy(gameObject);
            ItemEvents.ItemCollected("potion");
        }
    }
}
