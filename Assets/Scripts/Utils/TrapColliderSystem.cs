using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapColliderSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entrou");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Morreu");
            GameController.Instance.GameOver();
        }
    }
}
