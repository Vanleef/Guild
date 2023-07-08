using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController Instance;
    public List<Vector3> unlockedCheckpoints;
    public Vector3 lastCheckpointPos;
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
        unlockedCheckpoints = new List<Vector3>();
        CheckpointEvents.OnCheckpointInteraction += CheckPointReached;
        DontDestroyOnLoad(this);
    }

    private void CheckPointReached(Checkpoint checkpoint)
    {
        if (!unlockedCheckpoints.Contains(checkpoint.transform.position))
        {
            unlockedCheckpoints.Add(checkpoint.transform.position);
            lastCheckpointPos = checkpoint.transform.position;
            Debug.Log("Reached new checkpoint");
        }
    }
}
