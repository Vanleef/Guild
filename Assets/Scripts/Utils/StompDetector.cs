using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompDetector : MonoBehaviour
{
    //ONLY TO BE USED ON WARRIOR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public GameObject stomper;

    // Start is called before the first frame update
    void Start()
    {
        //stomper = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        stomper.GetComponent<WarriorInputController>().stompDetected(col);
    }
}
