using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockController : MonoBehaviour
{

    public int unlockOnDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unlockProgress(int progressId){
        GameProgress.instance.setProgress(progressId, true);
    }

}
