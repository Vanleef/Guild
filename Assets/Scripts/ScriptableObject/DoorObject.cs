using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [SerializeField]
    private GameObject doorObj;
    [SerializeField]
    public int progressId;

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Player" && GameProgress.instance.getProgress(progressId)){
            doorObj.SetActive(false);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameProgress.instance.getProgress(progressId))
        {
            doorObj.SetActive(true);
        }
    }
}
