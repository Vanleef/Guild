using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField]
    private bool[] progress = new bool[100];
    public static GameProgress instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setProgress(int progressId, bool complete){
        progress[progressId] = complete;
    }

    public bool getProgress(int progressId){
        return progress[progressId];
    }

    public void resetProgress(){
        progress = new bool[100];
        progress[1] = true;
    }

}
