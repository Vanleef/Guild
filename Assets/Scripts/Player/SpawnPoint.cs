using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Transform player;
    public float distanceToSpawn;       // Distance from player
    public int npcNumber;               // Number of NPCs to spawn
    private int currentNumber;
    public GameObject npc;              // NPC object
    private GameObject currentNpc;
    private float timeToSpawn;
    [SerializeField]
    private int unlockOnDeath;
    [SerializeField]
    private int lockOnSpawn = 99999;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentNumber = 0;
        timeToSpawn = 1f;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < distanceToSpawn) {
            if (currentNumber < npcNumber) {
                if (timeToSpawn <= 0) {
                    currentNpc = Instantiate(npc, transform.position, transform.rotation);
                    if(currentNpc.gameObject.GetComponent<UnlockController>() != null){
                        currentNpc.gameObject.GetComponent<UnlockController>().unlockOnDeath = unlockOnDeath;
                    }
                    currentNumber++;
                    timeToSpawn = 1f;
                    if(lockOnSpawn != 99999){
                        GameProgress.instance.setProgress(lockOnSpawn, false);
                    }
                } else {
                    timeToSpawn -= Time.deltaTime;
                }
            } else {
                if (currentNpc == false) {
                    currentNumber = 0;
                }
            }
        }
    }
}
